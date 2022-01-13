namespace BeautyAndThePet.Web.ViewModels
{
    using System;

    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Mapping;

    public class AdViewModel : IMapFrom<Ad>
    {
        public string ApplicationUser { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
