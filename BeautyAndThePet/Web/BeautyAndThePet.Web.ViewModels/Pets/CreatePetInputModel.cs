namespace BeautyAndThePet.Web.ViewModels.Pets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Data.Models.Enumerations;
    using Microsoft.AspNetCore.Http;

    public class CreatePetInputModel
    {
        /*
        var viewModel = new CreatePetInputModel
        {
            Name = "Kapitan Salam",
            Sex = Sex.Male,
            TypeOfPet = Data.Models.Enumerations.TypeOfPet.Dog, // ??????????
            Breed = "Pincher Ninja",
            BirthDate = DateTime.UtcNow.Date,
            Description = "Very Aggressive",
        };
        */

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public TypeOfPet TypeOfPet { get; set; }

        // Breeds should show as a list in forms
        public string Breed { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AvailableFrom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AvailableTo { get; set; }

        public string Description { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
