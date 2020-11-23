namespace BeautyAndThePet.Data.Models
{
    using System;

    using BeautyAndThePet.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public virtual ApplicationUser From { get; set; }

        public virtual ApplicationUser To { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
