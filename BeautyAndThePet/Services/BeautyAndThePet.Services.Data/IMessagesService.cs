﻿namespace BeautyAndThePet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BeautyAndThePet.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task CreateSentMessageAsync(MessageInputViewModel input, string userId,string ownerId);

        Task CreateReceivedMessageAsync(MessageInputViewModel input, string userId, string ownerId);

        IEnumerable<MessageViewModel> GetReceivedMessages(string userId);

        IEnumerable<MessageViewModel> GetSentMessages(string userId);
    }
}
