namespace BeautyAndThePet.Web.ViewModels.Pets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Data.Models.Enumerations;
    using BeautyAndThePet.Services.Mapping;

    public class EditPetInputModel : IMapFrom<Pet>, IValidatableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual TypeOfPet TypeOfPet { get; set; }

        public int BreedId { get; set; }

        public virtual Sex Sex { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartOfPeriod { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndOfPeriod { get; set; }

        public string Description { get; set; }

        public IEnumerable<BreedDropDownViewModel> Breeds { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
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
