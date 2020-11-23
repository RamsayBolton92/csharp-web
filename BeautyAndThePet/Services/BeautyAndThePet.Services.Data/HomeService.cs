using BeautyAndThePet.Common;
using BeautyAndThePet.Data.Common.Repositories;
using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeautyAndThePet.Services.Data
{
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
