﻿namespace BeautyAndThePet.Data.Models
{
    using System;

    using BeautyAndThePet.Data.Common.Models;

    public class ReceivedMessage : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string From { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }

        public bool IsRead { get; set; }
    }
}
