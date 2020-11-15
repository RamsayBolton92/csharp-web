using System;

namespace BeautyAndThePet.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        public virtual ApplicationUser From { get; set; }

        public virtual ApplicationUser To { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
