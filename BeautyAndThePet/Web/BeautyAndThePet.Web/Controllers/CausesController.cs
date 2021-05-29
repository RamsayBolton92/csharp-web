namespace BeautyAndThePet.Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Data;
    using BeautyAndThePet.Web.ViewModels.Causes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CausesController : Controller
    {
        private readonly ICausesService causesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public CausesController(ICausesService causesService, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            this.causesService = causesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new CreateCauseInputModel
            {
                Title = "Save the dolphins",
                Description = "The population of dophins is highly decreasing last couple of years...",
                Funds = 1_000_000,
                BankAccount = "ABC123ABC123",
                AccountOwner = "Johnny Walker",
                Creator = user.UserName,
                StartOfPeriod = DateTime.Parse("01.01.2021", CultureInfo.InvariantCulture),
                EndOfPeriod = DateTime.Parse("02.11.2021", CultureInfo.InvariantCulture),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCauseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.causesService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(input);
            }

            this.TempData["Message"] = "Cause added successfully.";

            return this.RedirectToAction("All");
        }

        [Authorize]
        public IActionResult All()
        {
            var allCausesViewModel = new AllCausesListViewModel()
            {
                AllCauses = this.causesService.GetAll(),
            };

            return this.View(allCausesViewModel);
        }

        [Authorize]
        public IActionResult ViewCauseInfo(int id)
        {
            var chosenCauseView = this.causesService.GetById<CauseViewModel>(id);

            return this.View(chosenCauseView);
        }

        [Authorize]
        public async Task<IActionResult> MyCauses()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var petsViewModel = new MyCausesListViewModel() { MyCauses = this.causesService.GetMyCauses(user.Id) };

            return this.View(petsViewModel);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var inputModel = this.causesService.GetById<EditCauseInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditCauseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.causesService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.ViewCauseInfo), new { id });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {

            await this.causesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.MyCauses));
        }
    }
}