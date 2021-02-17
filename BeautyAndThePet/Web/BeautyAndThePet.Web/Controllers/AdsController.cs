using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Services.Data;
using BeautyAndThePet.Web.ViewModels.Ads;

namespace BeautyAndThePet.Web.Controllers
{
    public class AdsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdsService adsService;

        public AdsController(UserManager<ApplicationUser> userManager, IAdsService adsService)
        {
            this.userManager = userManager;
            this.adsService = adsService;
        }

        [Authorize]
        public IActionResult New(int id)
        {
            var user = this.User.Identity.Name;

            var adInput = new AdInputViewModel { From = user, SentOn = DateTime.UtcNow };

            return this.View(adInput);
        }
    }
}
