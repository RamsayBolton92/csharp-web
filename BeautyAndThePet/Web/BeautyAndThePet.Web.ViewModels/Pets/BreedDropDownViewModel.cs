namespace BeautyAndThePet.Web.ViewModels.Pets
{
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Mapping;

    public class BreedDropDownViewModel : IMapFrom<Breed>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
