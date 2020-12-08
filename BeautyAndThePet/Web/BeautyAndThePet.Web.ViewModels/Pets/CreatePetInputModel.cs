namespace BeautyAndThePet.Web.ViewModels.Pets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BeautyAndThePet.Data.Models.Enumerations;
    using Microsoft.AspNetCore.Http;

    public class CreatePetInputModel : IValidatableObject
    {
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
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public string Description { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Start > this.End)
            {
                yield return new ValidationResult("End date should be later than start date");
            }

            if (this.Start < this.BirthDate)
            {
                yield return new ValidationResult("Start date should be later than birthday");
            }
        }
    }
}
