﻿using Microsoft.AspNetCore.Mvc;

namespace BeautyAndThePet.Web.Controllers
{
    public class PetsController : Controller
    {
        public IActionResult Create()
        {
            return this.View();
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
