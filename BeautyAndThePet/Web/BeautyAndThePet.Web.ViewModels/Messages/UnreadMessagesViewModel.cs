using System;
using System.Collections.Generic;
using System.Text;

namespace BeautyAndThePet.Web.ViewModels.Messages
{
    public class UnreadMessagesViewModel
    {
        public virtual string From { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
