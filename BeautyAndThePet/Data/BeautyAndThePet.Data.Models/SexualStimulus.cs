namespace BeautyAndThePet.Data.Models
{
    using BeautyAndThePet.Data.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SexualStimulus : BaseDeletableModel<int>
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        // In VM
        // public string Duration => $"{this.Start}-{this.End}";

        [ForeignKey("Pet")]
        public int PetId { get; set; }

        public virtual Pet Pet { get; set; }
    }
}
