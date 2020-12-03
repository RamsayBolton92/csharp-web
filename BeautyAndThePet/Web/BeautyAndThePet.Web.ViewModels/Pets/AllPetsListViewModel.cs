namespace BeautyAndThePet.Web.ViewModels.Pets
{
    using System;
    using System.Collections.Generic;

    public class AllPetsListViewModel
    {
        public IEnumerable<PetInListViewModel> AllPets { get; set; }

        public int Page { get; set; }

        public int PetsCount { get; set; }

        public int PetsPerPage { get; set; }

        public bool HasPrevousPage => this.Page > 1;

        public int PrevousPageNumber => this.Page - 1;

        public bool HasNextPage => this.Page < this.PagesCount;

        public int NextPageNumber => this.Page + 1;

        public int PagesCount => (int)Math.Ceiling((decimal)this.PetsCount / this.PetsPerPage);
    }
}
