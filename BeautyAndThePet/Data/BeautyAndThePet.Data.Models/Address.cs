namespace BeautyAndThePet.Data.Models
{
    using System.Collections.Generic;

    public class Address
    {
        public Address()
        {
            this.Habitants = new HashSet<User>();
        }

        public string Country { get; set; }

        public string Town { get; set; }

        public string Neighbourhood { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public virtual ICollection<User> Habitants { get; set; }
    }
}
