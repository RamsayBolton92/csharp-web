namespace BeautyAndThePet.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BeautyAndThePet.Data.Common.Models;
    using BeautyAndThePet.Data.Models.Enumerations;

    public class Pet : BaseDeletableModel<int>
    {
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime BirthDate { get; set; }

		public string Avatar { get; set; }

		public string Description { get; set; }

		public virtual TypeOfPet TypeOfPet { get; set; }

        public int OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public int BreedId { get; set; }

        public virtual Breed Breed { get; set; }

		[ForeignKey("SexualStimulus")]
        public int SexualStimulusId { get; set; }

        public virtual SexualStimulus SexualStimulus { get; set; }

        public virtual Sex Sex { get; set; }
	}
}
