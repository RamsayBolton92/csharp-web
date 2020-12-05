namespace BeautyAndThePet.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Data.Models.Enumerations;
    using BeautyAndThePet.Services.Data;
    using BeautyAndThePet.Web.ViewModels.Pets;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PetsController : Controller
    {
        private readonly IPetsService petsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public PetsController(IPetsService petsService, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            this.petsService = petsService;
            this.userManager = userManager;
            this.environment = environment;
        }


        public IActionResult Create()
        {
            var viewModel = new CreatePetInputModel
            {
                Name = "Kapitan Salam",
                Sex = Sex.Male,
                TypeOfPet = TypeOfPet.Dog,
                Breed = "Pincher ninja",
                BirthDate = DateTime.UtcNow,
                Description = "Very Aggressive",
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.petsService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction("MyPets");
        }

        public async Task<IActionResult> MyPets()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var petsViewModel = new MyPetsListViewModel() { MyPets = this.petsService.GetMyPets(user.Id) };

            return this.View(petsViewModel);
        }

        public IActionResult All(int id)
        {
            const int PetsPerPage = 10;

            var allPetsViewModel = new AllPetsListViewModel()
            {
                Page = id,
                PetsCount = this.petsService.GetCount(),
                PetsPerPage = PetsPerPage,
                AllPets = this.petsService.GetAll(id, PetsPerPage),
            };

            return this.View(allPetsViewModel);
        }

        public IActionResult MatchedPets()
        {
            return this.View();
        }

        public IActionResult TopTen()
        {
            return this.View();
        }
    }
}
