using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Services.Data;
using BeautyAndThePet.Web.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyAndThePet.Web.Controllers
{
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
