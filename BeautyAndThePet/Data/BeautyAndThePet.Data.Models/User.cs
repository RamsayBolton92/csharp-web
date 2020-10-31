namespace BeautyAndThePet.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
    }
}
