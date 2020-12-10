namespace BeautyAndThePet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using BeautyAndThePet.Data.Common.Models;
    using BeautyAndThePet.Data.Models.Enumerations;

    public class Pet : BaseDeletableModel<int>
    {
        public Pet()
        {
            this.Images = new HashSet<Image>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Description { get; set; }

        public virtual TypeOfPet TypeOfPet { get; set; }

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public int BreedId { get; set; }

        public virtual Breed Breed { get; set; }


        public virtual SexualStimulus SexualStimulus { get; set; }

        public virtual Sex Sex { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
