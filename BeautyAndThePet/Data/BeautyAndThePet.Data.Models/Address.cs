namespace BeautyAndThePet.Data.Models
{
    using BeautyAndThePet.Data.Common.Models;
    using System.Collections.Generic;

    public class Address : BaseDeletableModel<int>
    {
        public Address()
        {
            this.Habitants = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public string Neighbourhood { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public virtual ICollection<ApplicationUser> Habitants { get; set; }
    }
}
