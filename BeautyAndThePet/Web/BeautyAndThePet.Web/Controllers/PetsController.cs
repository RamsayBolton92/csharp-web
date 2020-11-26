﻿namespace BeautyAndThePet.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Models.Enumerations;
    using BeautyAndThePet.Services.Data;
    using BeautyAndThePet.Web.ViewModels.Pets;
    using Microsoft.AspNetCore.Mvc;

    public class PetsController : Controller
    {
        private readonly IPetsService petsService;

        public PetsController(IPetsService petsService)
        {
            this.petsService = petsService;
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
                return this.View();
            }

            await this.petsService.CreateAsync(input);
            return this.RedirectToAction("MyPets");
        }

        public IActionResult MyPets()
        {
            return this.View();
        }

        public IActionResult All()
        {
            return this.View();
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
