namespace BeautyAndThePet.Services.Data
{
    using System.Linq;

    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Web.ViewModels.Home;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<About> aboutsRepo;

        public HomeService(IDeletableEntityRepository<About> aboutsRepo)
        {
            this.aboutsRepo = aboutsRepo;
        }

        public IndexViewModel Index()
        {
            var viewModel = new IndexViewModel() { AboutUs = this.aboutsRepo.All().FirstOrDefault().Description };

            return viewModel;
        }
    }
}
