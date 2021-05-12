using BeautyAndThePet.Data.Common.Repositories;
using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Services.Mapping;
using BeautyAndThePet.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyAndThePet.Web.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Ad> adRepo;

        public SidebarViewComponent(IDeletableEntityRepository<Ad> adRepo)
        {
            this.adRepo = adRepo;
        }

        public IViewComponentResult Invoke()
        {
            var model = new SidebarViewModel 
            {
                Ads = this.adRepo.All().OrderByDescending(x => x.CreatedOn).To<AdViewModel>().Take(5).ToList(),
            };

            return this.View(model);
        }
    }
}
