﻿using System;

namespace BeautyAndThePet.Web.ViewModels.Messages
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
