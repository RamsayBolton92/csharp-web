namespace BeautyAndThePet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BeautyAndThePet.Web.ViewModels.Causes;

    public interface ICausesService
    {
        Task CreateAsync(CreateCauseInputModel input, string userId, string imagePath);

        IEnumerable<CauseViewModel> GetAll();

        T GetById<T>(int id);

        IEnumerable<CauseViewModel> GetMyCauses(string userId);

        Task DeleteAsync(int id);

        Task UpdateAsync(int id, EditCauseInputModel input);
    }
}
