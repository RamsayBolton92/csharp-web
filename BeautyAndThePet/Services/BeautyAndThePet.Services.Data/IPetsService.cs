namespace BeautyAndThePet.Services.Data
{
    using System.Threading.Tasks;

    using BeautyAndThePet.Web.ViewModels.Pets;

    public interface IPetsService
    {
        Task CreateAsync(CreatePetInputModel input, string userId);
    }
}
