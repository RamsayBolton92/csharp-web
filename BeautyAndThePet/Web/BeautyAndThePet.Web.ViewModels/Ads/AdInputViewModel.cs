namespace BeautyAndThePet.Web.ViewModels.Ads
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AdInputViewModel
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public virtual string ApplicationUser { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(800)]
        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
