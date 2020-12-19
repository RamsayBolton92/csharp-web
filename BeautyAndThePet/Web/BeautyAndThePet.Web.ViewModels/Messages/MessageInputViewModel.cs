namespace BeautyAndThePet.Web.ViewModels.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MessageInputViewModel
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public virtual string From { get; set; }

        [MaxLength(30)]
        public virtual string To { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(800)]
        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
