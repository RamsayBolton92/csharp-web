namespace BeautyAndThePet.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Data;
    using BeautyAndThePet.Web.ViewModels;
    using BeautyAndThePet.Web.ViewModels.Ads;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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

            var adInput = new AdInputViewModel { ApplicationUser = user, SentOn = DateTime.UtcNow };

            return this.View(adInput);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> New(AdInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.adsService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(input);
            }

            this.TempData["Message"] = "Ad added successfully.";

            return this.RedirectToAction("MyPets","Pets");
        }

        [Authorize]
        public IActionResult All()
        {
            var allAds = new AdsListViewModel()
            {
                Ads = this.adsService.GetAll(),
            };

            return this.View(allAds);
        }
    }
}