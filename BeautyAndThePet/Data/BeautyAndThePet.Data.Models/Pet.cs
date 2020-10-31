namespace BeautyAndThePet.Data.Models
{
    using BeautyAndThePet.Data.Models.Enumerations;
    using System;

	public class Pet
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime BirthDate { get; set; }

		public string Sex { get; set; }

		public string Avatar { get; set; }

		public string Description { get; set; }

		public TypeOfPet TypeOfPet{ get; set; }

        public virtual User Owner { get; set; }

		public virtual Breed Breed { get; set; }

		public virtual SexualStimulus SexualStimulus { get; set; }
	}
}
