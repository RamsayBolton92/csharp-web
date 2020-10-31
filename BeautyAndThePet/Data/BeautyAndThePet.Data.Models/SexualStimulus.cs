namespace BeautyAndThePet.Data.Models
{
    using System;

    public class SexualStimulus
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Duration => $"{this.Start}-{this.End}";

        public virtual Pet Pet { get; set; }
    }
}
