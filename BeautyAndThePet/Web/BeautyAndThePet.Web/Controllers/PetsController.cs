namespace BeautyAndThePet.Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Data.Models.Enumerations;
    using BeautyAndThePet.Services.Data;
    using BeautyAndThePet.Web.ViewModels.Pets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PetsController : Controller
    {
        private readonly IPetsService petsService;
        private readonly IBreedsService breedsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public PetsController(IPetsService petsService, IBreedsService breedsService, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            this.petsService = petsService;
            this.breedsService = breedsService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Create()
        {
            var breeds = this.breedsService.GetAll<BreedDropDownViewModel>();

            var viewModel = new CreatePetInputModel
            {
                Name = "Kapitan Salam",
                Sex = Sex.Male,
                TypeOfPet = TypeOfPet.Dog,
                Breeds = breeds,
                BirthDate = DateTime.Parse("01.01.2021", CultureInfo.InvariantCulture),
                Description = "Very Aggressive",
                StartOfPeriod = DateTime.Parse("01.01.2021", CultureInfo.InvariantCulture),
                EndOfPeriod = DateTime.Parse("02.01.2021", CultureInfo.InvariantCulture),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var breeds = this.breedsService.GetAll<BreedDropDownViewModel>();
                input.Breeds = breeds;

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
                input.Breeds = this.breedsService.GetAll<BreedDropDownViewModel>();

                return this.View(input);
            }

            this.TempData["Message"] = "Pet added successfully.";

            return this.RedirectToAction("MyPets");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var breeds = this.breedsService.GetAll<BreedDropDownViewModel>();

            var inputModel = this.petsService.GetById<EditPetInputModel>(id);
            inputModel.Breeds = breeds;

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditPetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var breeds = this.breedsService.GetAll<BreedDropDownViewModel>();
                input.Breeds = breeds;
                return this.View(input);
            }

            await this.petsService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.ViewPetInfo), new { id });
        }

        [Authorize]
        public async Task<IActionResult> MyPets()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var petsViewModel = new MyPetsListViewModel() { MyPets = this.petsService.GetMyPets(user.Id) };

            return this.View(petsViewModel);
        }

        [Authorize]
        public IActionResult All(int id = 1)
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

        [Authorize]
        public async Task<IActionResult> MatchedPets(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var pet = this.petsService.GetById<PetViewModel>(id);

            var matchedPetsViewModel = new MatchedPetsListViewModel()
            {
               MatchedPets = this.petsService.GetMatchedPets(pet.Id, user.Id),
            };

            return this.View(matchedPetsViewModel);
        }

        [Authorize]
        public IActionResult ViewPetInfo(int id)
        {
            var chosenPetView = this.petsService.GetById<PetViewModel>(id);

            return this.View(chosenPetView);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {

            await this.petsService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.MyPets));
        }

        [Authorize]
        public IActionResult AllMaleDogs(int id = 1)
        {
            const int PetsPerPage = 10;

            var allPetsViewModel = new AllPetsListViewModel()
            {
                Page = id,
                PetsCount = this.petsService.GetCount(),
                PetsPerPage = PetsPerPage,
                AllPets = this.petsService.GetAllMaleDogs(id, PetsPerPage),
            };

            return this.View(allPetsViewModel);
        }

        [Authorize]
        public IActionResult AllFemaleDogs(int id = 1)
        {
            const int PetsPerPage = 10;

            var allPetsViewModel = new AllPetsListViewModel()
            {
                Page = id,
                PetsCount = this.petsService.GetCount(),
                PetsPerPage = PetsPerPage,
                AllPets = this.petsService.GetAllFemaleDogs(id, PetsPerPage),
            };

            return this.View(allPetsViewModel);
        }

        [Authorize]
        public IActionResult AllMaleCats(int id = 1)
        {
            const int PetsPerPage = 10;

            var allPetsViewModel = new AllPetsListViewModel()
            {
                Page = id,
                PetsCount = this.petsService.GetCount(),
                PetsPerPage = PetsPerPage,
                AllPets = this.petsService.GetAllMaleCats(id, PetsPerPage),
            };

            return this.View(allPetsViewModel);
        }

        [Authorize]
        public IActionResult AllFemaleCats(int id = 1)
        {
            const int PetsPerPage = 10;

            var allPetsViewModel = new AllPetsListViewModel()
            {
                Page = id,
                PetsCount = this.petsService.GetCount(),
                PetsPerPage = PetsPerPage,
                AllPets = this.petsService.GetAllFemaleCats(id, PetsPerPage),
            };

            return this.View(allPetsViewModel);
        }

        [Authorize]
        public IActionResult ByCreationDateAscending(int id = 1)
        {
            const int PetsPerPage = 10;

            var allPetsViewModel = new AllPetsListViewModel()
            {
                Page = id,
                PetsCount = this.petsService.GetCount(),
                PetsPerPage = PetsPerPage,
                AllPets = this.petsService.GetAllPetsByCreationDateAscending(id, PetsPerPage),
            };

            return this.View(allPetsViewModel);
        }

        [Authorize]
        public IActionResult ByCreationDateDescending(int id = 1)
        {
            const int PetsPerPage = 10;

            var allPetsViewModel = new AllPetsListViewModel()
            {
                Page = id,
                PetsCount = this.petsService.GetCount(),
                PetsPerPage = PetsPerPage,
                AllPets = this.petsService.GetAllPetsByCreationDateDescending(id, PetsPerPage),
            };

            return this.View(allPetsViewModel);
        }
    }
}