namespace BeautyAndThePet.Web.ViewModels.Messages
{
    using System;

    public class AllRecievedMessagesViewModel
    {
        public virtual string From { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
