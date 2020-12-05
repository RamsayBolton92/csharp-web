namespace BeautyAndThePet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Mapping;
    using BeautyAndThePet.Web.ViewModels.Pets;

    public class PetsService : IPetsService
    {
        private readonly IDeletableEntityRepository<Breed> breedsRepo;
        private readonly IDeletableEntityRepository<Pet> petsRepo;
        private readonly string[] allowedExtensions = new[] { "jpg", "png"};

        public PetsService(IDeletableEntityRepository<Breed> breedsRepo, IDeletableEntityRepository<Pet> petsRepo)
        {
            this.breedsRepo = breedsRepo;
            this.petsRepo = petsRepo;
        }

        public async Task CreateAsync(CreatePetInputModel input, string userId, string imagePath)
        {
            var pet = new Pet()
            {
                Name = input.Name,
                Sex = input.Sex,
                TypeOfPet = input.TypeOfPet,
                Breed = this.breedsRepo.All().FirstOrDefault(x => x.Name == input.Breed),
                BirthDate = input.BirthDate,
                SexualStimulus = new SexualStimulus() { Start = input.Start, End = input.End },
                Description = input.Description,
                OwnerId = userId,
            };

            // /wwwroot/images/recipes/jhdsi-343g3h453-=g34g.jpg
            Directory.CreateDirectory($"{imagePath}/pets/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };

                pet.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/pets/{dbImage.Id}.{extension}";

                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.petsRepo.AddAsync(pet);
            await this.petsRepo.SaveChangesAsync();
        }

        public IEnumerable<PetInListViewModel> GetAll(int pageId, int petsPerPage = 10)
        {
            //var recipes = this.recipesRepository.AllAsNoTracking()
            //    .OrderByDescending(x => x.Id)
            //    .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
            //    .To<T>().ToList();
            //return recipes;

            var pets = this.petsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((pageId - 1) * petsPerPage).Take(petsPerPage)
                .To<PetInListViewModel>().ToList();

            return pets;
        }

        public int GetCount()
        {
            return this.petsRepo.All().Count();
        }

        public IEnumerable<PetInListViewModel> GetMyPets(string userId)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .Where(x => x.OwnerId == userId)
                .OrderByDescending(x => x.Id)
                .To<PetInListViewModel>().ToList();

            return pets;
        }
    }
}
