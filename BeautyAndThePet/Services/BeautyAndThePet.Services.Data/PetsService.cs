﻿namespace BeautyAndThePet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Data.Models.Enumerations;
    using BeautyAndThePet.Services.Mapping;
    using BeautyAndThePet.Web.ViewModels.Pets;

    public class PetsService : IPetsService
    {
        private readonly IDeletableEntityRepository<Breed> breedsRepo;
        private readonly IDeletableEntityRepository<Pet> petsRepo;
        private readonly string[] allowedExtensions = new[] { "jpg", "png" };

        public PetsService(IDeletableEntityRepository<Breed> breedsRepo, IDeletableEntityRepository<Pet> petsRepo)
        {
            this.breedsRepo = breedsRepo;
            this.petsRepo = petsRepo;
        }

        public async Task CreateAsync(CreatePetInputModel input, string userId, string imagePath)
        {
            var breed = this.breedsRepo.All().FirstOrDefault(x => x.Id == input.BreedId);

            if (input.TypeOfPet != breed.TypeOfPet)
            {
                throw new Exception($"Invalid breed! Choose valid {input.TypeOfPet} breed!");
            }

            var pet = new Pet()
            {
                Name = input.Name,
                Sex = input.Sex,
                TypeOfPet = input.TypeOfPet,
                BreedId = input.BreedId,
                BirthDate = input.BirthDate,
                StartOfPeriod = input.StartOfPeriod,
                EndOfPeriod = input.EndOfPeriod,
                Description = input.Description,
                OwnerId = userId,
            };
            Directory.CreateDirectory($"{imagePath}/pets/");
            foreach (var image in input.Images)
            {
                    var extension = Path.GetExtension(image.FileName).TrimStart('.');

                    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Invalid image extension {extension}");
                    }

                    var dbImage = new PetImage
                    {
                        AddedByUserId = userId,
                        Extension = extension,
                    };

                    pet.PetImages.Add(dbImage);

                    var physicalPath = $"{imagePath}/pets/{dbImage.Id}.{extension}";

                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                }

            await this.petsRepo.AddAsync(pet);
            await this.petsRepo.SaveChangesAsync();
        }

        public IEnumerable<PetViewModel> GetAll(int pageId, int petsPerPage = 10)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((pageId - 1) * petsPerPage).Take(petsPerPage)
                .To<PetViewModel>().ToList();

            return pets;
        }

        public int GetCount()
        {
            return this.petsRepo.All().Count();
        }

        public IEnumerable<PetViewModel> GetMatchedPets(int id, string userId)
        {
            var myPet = this.petsRepo.All()
                .Where(x => x.OwnerId == userId)
                .FirstOrDefault(x => x.Id == id);

            var pets = this.petsRepo.All()
                .Where(x => x.OwnerId != userId && x.BreedId == myPet.BreedId)
                .To<PetViewModel>().ToList();

            return pets;
        }

        public IEnumerable<PetViewModel> GetMyPets(string userId)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .Where(x => x.OwnerId == userId)
                .OrderByDescending(x => x.Id)
                .To<PetViewModel>().ToList();

            return pets;
        }

        public T GetById<T>(int id)
        {
            var pet = this.petsRepo.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return pet;
        }

        public async Task UpdateAsync(int id, EditPetInputModel input)
        {
            var pet = this.petsRepo.All().FirstOrDefault(x => x.Id == id);

            pet.Name = input.Name;
            pet.BirthDate = input.BirthDate;
            pet.TypeOfPet = input.TypeOfPet;
            pet.BreedId = input.BreedId;
            pet.Description = input.Description;
            pet.Sex = input.Sex;
            pet.StartOfPeriod = input.StartOfPeriod;
            pet.EndOfPeriod = input.EndOfPeriod;

            await this.petsRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pet = this.petsRepo.All().FirstOrDefault(x => x.Id == id);

            pet.IsDeleted = true;

            await this.petsRepo.SaveChangesAsync();
        }

        public IEnumerable<PetViewModel> GetAllMaleDogs(int pageId, int petsPerPage = 10)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.Id).Where(p => p.Sex == Sex.Male && p.TypeOfPet == TypeOfPet.Dog)
                .Skip((pageId - 1) * petsPerPage).Take(petsPerPage)
                .To<PetViewModel>().ToList();

            return pets;
        }

        public IEnumerable<PetViewModel> GetAllFemaleDogs(int pageId, int petsPerPage = 10)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.Id).Where(p => p.Sex == Sex.Female && p.TypeOfPet == TypeOfPet.Dog)
                .Skip((pageId - 1) * petsPerPage).Take(petsPerPage)
                .To<PetViewModel>().ToList();

            return pets;
        }

        public IEnumerable<PetViewModel> GetAllMaleCats(int pageId, int petsPerPage = 10)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.Id).Where(p => p.Sex == Sex.Male && p.TypeOfPet == TypeOfPet.Cat)
                .Skip((pageId - 1) * petsPerPage).Take(petsPerPage)
                .To<PetViewModel>().ToList();

            return pets;
        }

        public IEnumerable<PetViewModel> GetAllFemaleCats(int pageId, int petsPerPage = 10)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.Id).Where(p => p.Sex == Sex.Female && p.TypeOfPet == TypeOfPet.Cat)
                .Skip((pageId - 1) * petsPerPage).Take(petsPerPage)
                .To<PetViewModel>().ToList();

            return pets;
        }

        public IEnumerable<PetViewModel> GetAllPetsByCreationDateAscending(int pageId, int petsPerPage = 10)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .OrderBy(x => x.CreatedOn)
                .Skip((pageId - 1) * petsPerPage).Take(petsPerPage)
                .To<PetViewModel>().ToList();

            return pets;
        }

        public IEnumerable<PetViewModel> GetAllPetsByCreationDateDescending(int pageId, int petsPerPage = 10)
        {
            var pets = this.petsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((pageId - 1) * petsPerPage).Take(petsPerPage)
                .To<PetViewModel>().ToList();

            return pets;
        }
    }
}
