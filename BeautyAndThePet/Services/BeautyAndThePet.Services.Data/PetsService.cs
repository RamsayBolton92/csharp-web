namespace BeautyAndThePet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Web.ViewModels.Pets;

    public class PetsService : IPetsService
    {
        private readonly IDeletableEntityRepository<Breed> breedsRepo;
        private readonly IDeletableEntityRepository<Pet> petsRepo;

        public PetsService(IDeletableEntityRepository<Breed> breedsRepo, IDeletableEntityRepository<Pet> petsRepo)
        {
            this.breedsRepo = breedsRepo;
            this.petsRepo = petsRepo;
        }

        public async Task CreateAsync(CreatePetInputModel input, string userId)
        {
            var pet = new Pet()
            {
                Name = input.Name,
                Sex = input.Sex,
                TypeOfPet = input.TypeOfPet,
                Breed = this.breedsRepo.All().FirstOrDefault(x => x.Name == input.Breed),
                BirthDate = input.BirthDate,
                SexualStimulus = new SexualStimulus() { Start = input.AvailableFrom, End = input.AvailableTo },
                Description = input.Description,
                OwnerId = userId,
            };

            await this.petsRepo.AddAsync(pet);
            await this.petsRepo.SaveChangesAsync();
        }

        public IEnumerable<PetInListViewModel> GetAll(int pageId, int petsPerPage = 10)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((pageId - 1) * petsPerPage)
                .Take(petsPerPage)
                .Select(x => new PetInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Breed = x.Breed.Name,
                    Sex = x.Sex.ToString(),
                    TypeOfPet = x.TypeOfPet.ToString(),
                    AvailableFrom = x.SexualStimulus.Start.ToString(),
                    AvailableTo = x.SexualStimulus.End.ToString(),
                }).ToList();

            return pets;
        }

        public int GetCount()
        {
            return this.petsRepo.All().Count();
        }

        public IEnumerable<MyPetInListViewModel> GetMyPets(string userId)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .Where(x => x.OwnerId == userId)
                .OrderByDescending(x => x.Id)
                .Select(x => new MyPetInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Breed = x.Breed.Name,
                    TypeOfPet = x.TypeOfPet.ToString(),
                    Sex = x.Sex.ToString(),
                    AvailableFrom = x.SexualStimulus.Start.ToString(),
                    AvailableTo = x.SexualStimulus.End.ToString(),
                }).ToList();

            return pets;
        }
    }
}
