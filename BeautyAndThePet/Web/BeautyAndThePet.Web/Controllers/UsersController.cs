namespace BeautyAndThePet.Web.Controllers
{
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public UsersController(UserManager<ApplicationUser> userManager, IUsersService usersService)
        {
            this.userManager = userManager;
            this.usersService = usersService;
        }

        [Authorize]
        public async Task<IActionResult> ViewInfo()
        {
            string absolutepath = this.HttpContext.Request.Path;

            string username = absolutepath.Replace("/Users/ViewInfo/", string.Empty);

            var user = await this.userManager.FindByNameAsync(username);

            var chosenUserView = this.usersService.GetUserInfo(user);

            return this.View(chosenUserView);
        }
    }
}
