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

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartOfPeriod { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndOfPeriod { get; set; }

        public string Description { get; set; }
        
        [Required]
        public IEnumerable<IFormFile> Images { get; set; }

        [Range(1, int.MaxValue)]
        public int BreedId { get; set; }

        public IEnumerable<BreedDropDownViewModel> Breeds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartOfPeriod > this.EndOfPeriod)
            {
                yield return new ValidationResult("End date should be later than start date");
            }

            if (this.StartOfPeriod > this.EndOfPeriod)
            {
                yield return new ValidationResult("End date should be later than start date");
            }

            if (this.StartOfPeriod < this.BirthDate)
            {
                yield return new ValidationResult("Start date should be later than birthday");
            }
        }
    }
}
