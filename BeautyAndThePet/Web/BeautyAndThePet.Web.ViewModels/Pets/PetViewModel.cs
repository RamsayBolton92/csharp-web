namespace BeautyAndThePet.Web.ViewModels.Pets
{
    using System.Linq;

    using AutoMapper;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Mapping;

    public class PetViewModel : IMapFrom<Pet>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TypeOfPet { get; set; }

        public string BreedName { get; set; }

        public string Sex { get; set; }

        public string SexualStimulusStart { get; set; }

        public string SexualStimulusEnd { get; set; }

        public string BirthDate { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public string OwnerUserName { get; set; }

        public string OwnerAddressTown { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, PetViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        "/images/pets/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
