namespace BeautyAndThePet.Web.ViewModels.Pets
{
    using System.Collections.Generic;

    public class MyPetsListViewModel
    {
        public IEnumerable<MyPetInListViewModel> MyPets { get; set; }
    }
}
