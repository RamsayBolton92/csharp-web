namespace BeautyAndThePet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BeautyAndThePet.Web.ViewModels.Pets;

    public interface IPetsService
    {
        Task CreateAsync(CreatePetInputModel input, string userId, string imagePath);

        IEnumerable<PetInListViewModel> GetAll(int pageId, int petsPerPage = 10);

        IEnumerable<MyPetInListViewModel> GetMyPets(string userId);

        int GetCount();
    }
}
