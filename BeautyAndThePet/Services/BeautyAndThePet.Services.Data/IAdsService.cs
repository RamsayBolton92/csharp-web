namespace BeautyAndThePet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BeautyAndThePet.Web.ViewModels;
    using BeautyAndThePet.Web.ViewModels.Ads;

    public interface IAdsService
    {
        public Task CreateAsync(AdInputViewModel input, string userId);

        IEnumerable<AdViewModel> GetAll();
    }
}
