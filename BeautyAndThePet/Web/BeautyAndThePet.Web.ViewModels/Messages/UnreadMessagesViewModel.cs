namespace BeautyAndThePet.Web.ViewModels.Messages
{
    using System;

    public class UnreadMessagesViewModel
    {
        public virtual string From { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
