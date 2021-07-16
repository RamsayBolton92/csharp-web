namespace BeautyAndThePet.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using BeautyAndThePet.Common;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Data;
    using BeautyAndThePet.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IHomeService homeService, UserManager<ApplicationUser> userManager)
        {
            this.homeService = homeService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.Name == "fausts@abv.bg")
            {
                var user = await this.userManager.GetUserAsync(this.User);

                await this.userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
            }

            this.ViewBag.ShowSearchBar = true;

            var viewModel = this.homeService.Index();

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Chat()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}