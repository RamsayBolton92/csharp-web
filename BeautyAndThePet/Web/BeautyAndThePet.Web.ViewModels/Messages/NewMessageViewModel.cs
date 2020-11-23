namespace BeautyAndThePet.Web.ViewModels.Messages
{
    using System;

    public class NewMessageViewModel
    {
        public virtual string From { get; set; }

        public virtual string To { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
