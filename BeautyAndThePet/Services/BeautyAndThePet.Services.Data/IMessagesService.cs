namespace BeautyAndThePet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BeautyAndThePet.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task CreateSentMessageAsync(MessageInputViewModel input, string ownerId);

        Task CreateReceivedMessageAsync(MessageInputViewModel input, string userId);

        IEnumerable<MessageViewModel> GetReceivedMessages(string userId);
    }
}
