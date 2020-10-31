namespace BeautyAndThePet.Data.Models
{
    using System.Collections.Generic;

    public class Breed
    {
        public Breed()
        {
            this.Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Characteristics { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
