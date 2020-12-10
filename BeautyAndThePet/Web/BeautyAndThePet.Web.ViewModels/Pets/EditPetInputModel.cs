namespace BeautyAndThePet.Web.ViewModels.Pets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Data.Models.Enumerations;
    using BeautyAndThePet.Services.Mapping;

    public class EditPetInputModel : IMapFrom<Pet>, IHaveCustomMappings, IValidatableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual TypeOfPet TypeOfPet { get; set; }

        public int BreedId { get; set; }

        public virtual Sex Sex { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public string Description { get; set; }

        public IEnumerable<BreedDropDownViewModel> Breeds { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, EditPetInputModel>()
                .ForMember(x => x.Start, opt =>
                    opt.MapFrom(x => x.SexualStimulus.Start))
                .ForMember(x => x.End, opt =>
                    opt.MapFrom(x => x.SexualStimulus.End));
        }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
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
