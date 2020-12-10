namespace BeautyAndThePet.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BeautyAndThePet.Data.Common.Models;

    public class SexualStimulus : BaseDeletableModel<int>
    {
        [ForeignKey("Pet")]
        public int Id { get; set; }

        public DateTime Start { get; set; }

        // IN VIEW MODEL: public string Duration => $"{this.Start}-{this.End}";
        public DateTime End { get; set; }


        public virtual Pet Pet { get; set; }
    }
}
