using BeautyAndThePet.Web.ViewModels.Pets;
using Microsoft.AspNetCore.Mvc;
using BeautyAndThePet.Data.Models;
using System;

namespace BeautyAndThePet.Web.Controllers
{
    public class PetsController : Controller
    {
        public IActionResult Create()
        {
            var viewModel = new CreatePetInputModel
            {
                Name = "Kapitan Salam",
                Sex = Sex.Male,
                TypeOfPet = Data.Models.Enumerations.TypeOfPet.Dog, // ??????????
                Breed = "Pincher Ninja",
                BirthDate = DateTime.UtcNow.Date,
                Description = "Very Aggressive",
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create2()
        {
            if (!ModelState.IsValid)
            {

                return this.View();
            }

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
