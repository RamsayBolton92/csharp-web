﻿namespace BeautyAndThePet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BeautyAndThePet.Web.ViewModels.Pets;

    public interface IPetsService
    {
        Task CreateAsync(CreatePetInputModel input, string userId, string imagePath);

        IEnumerable<PetViewModel> GetAll(int pageId, int petsPerPage = 10);

        IEnumerable<PetViewModel> GetMyPets(string userId);

        IEnumerable<PetViewModel> GetMatchedPets(int id, string userId);

        int GetCount();

        Task UpdateAsync(int id, EditPetInputModel input);

        T GetById<T>(int id);

        Task DeleteAsync(int id);

        IEnumerable<PetViewModel> GetAllMaleDogs(int pageId, int petsPerPage = 10);

        IEnumerable<PetViewModel> GetAllFemaleDogs(int pageId, int petsPerPage = 10);

        IEnumerable<PetViewModel> GetAllMaleCats(int pageId, int petsPerPage = 10);

        IEnumerable<PetViewModel> GetAllFemaleCats(int pageId, int petsPerPage = 10);

        IEnumerable<PetViewModel> GetAllPetsByCreationDateAscending(int pageId, int petsPerPage = 10);

        IEnumerable<PetViewModel> GetAllPetsByCreationDateDescending(int pageId, int petsPerPage = 10);
    }
}
