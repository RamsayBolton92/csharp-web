using System;
using System.Collections.Generic;
using System.Text;

namespace BeautyAndThePet.Web.ViewModels.Pets
{
    public class CreatePetViewModel
    {
        public string Name { get; set; }

        public string BirthDate { get; set; }

        public string Description { get; set; }

        public int OwnerId { get; set; }

        public string Breed { get; set; }
        
        public string SexualStimulusId { get; set; }

        // public TypeOfPet TypeOfPet { get; set; }

        // public virtual ApplicationUser Owner { get; set; }

        // public string Avatar { get; set; }

       // public virtual Sex Sex { get; set; }
    }
}
