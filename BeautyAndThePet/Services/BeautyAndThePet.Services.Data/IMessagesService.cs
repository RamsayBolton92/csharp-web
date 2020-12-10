namespace BeautyAndThePet.Services.Data
{
    using BeautyAndThePet.Web.ViewModels.Messages;
    using System.Threading.Tasks;

    public interface IMessagesService
    {
        Task CreateSentMessageAsync(MessageInputViewModel input, string ownerId);

        Task CreateReceivedMessageAsync(MessageInputViewModel input, string userId);
    }
}
