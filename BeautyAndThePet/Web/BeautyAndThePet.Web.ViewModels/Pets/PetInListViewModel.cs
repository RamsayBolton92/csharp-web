namespace BeautyAndThePet.Web.ViewModels.Pets
{
    using System.Linq;

    using AutoMapper;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Mapping;

    public class PetInListViewModel : IMapFrom<Pet>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Breed { get; set; }

        public string Sex { get; set; }

        public string TypeOfPet { get; set; }

        public string SexualStimulusStart { get; set; }

        public string SexualStimulusEnd { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, PetInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        "~/images/pets/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
