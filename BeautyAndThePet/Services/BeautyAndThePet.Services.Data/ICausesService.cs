using BeautyAndThePet.Web.ViewModels.Causes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyAndThePet.Services.Data
{
    public interface ICausesService
    {
        Task CreateAsync(CreateCauseInputModel input, string userId, string imagePath);

        IEnumerable<CauseViewModel> GetAll();

        T GetById<T>(int id);

        IEnumerable<CauseViewModel> GetMyCauses(string userId);

        Task DeleteAsync(int id);
    }
}