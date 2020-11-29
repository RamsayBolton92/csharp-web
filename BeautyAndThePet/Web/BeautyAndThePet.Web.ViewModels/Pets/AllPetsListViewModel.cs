namespace BeautyAndThePet.Web.ViewModels.Pets
{
    using System.Collections.Generic;

    public class AllPetsListViewModel
    {
        public IEnumerable<PetInListViewModel> AllPets { get; set; }

        public int Page { get; set; }
    }
}
