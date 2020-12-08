using AutoMapper;
using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Data.Models.Enumerations;
using BeautyAndThePet.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautyAndThePet.Web.ViewModels.Pets
{
    public class EditPetInputModel : IMapFrom<Pet>, IHaveCustomMappings, IValidatableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual TypeOfPet TypeOfPet { get; set; }

        public virtual Sex Sex { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime SexualStimulusStart { get; set; }

        public DateTime SexualStimulusEnd { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, EditPetInputModel>()
                .ForMember(x => x.SexualStimulusStart, opt =>
                    opt.MapFrom(x => x.SexualStimulus.Start))
                .ForMember(x => x.SexualStimulusEnd, opt =>
                    opt.MapFrom(x => x.SexualStimulus.End));
        }

        

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (this.SexualStimulusStart > this.SexualStimulusEnd)
            {
                yield return new ValidationResult("End date should be later than start date");
            }

            if (this.SexualStimulusStart < this.BirthDate)
            {
                yield return new ValidationResult("Start date should be later than birthday");
            }
        }


        //public int BreedId { get; set; }

        //public virtual Breed Breed { get; set; }


    }
}
