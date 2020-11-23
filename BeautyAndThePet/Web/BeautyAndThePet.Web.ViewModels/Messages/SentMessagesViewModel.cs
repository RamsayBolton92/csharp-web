namespace BeautyAndThePet.Web.ViewModels.Messages
{
    using System;

    public class SentMessagesViewModel
    {
        public virtual string To { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
