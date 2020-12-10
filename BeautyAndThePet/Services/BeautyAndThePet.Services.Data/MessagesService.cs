namespace BeautyAndThePet.Services.Data
{
    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Web.ViewModels.Messages;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepo;
        private readonly IDeletableEntityRepository<SentMessage> sentMessagesRepo;
        private readonly IDeletableEntityRepository<ReceivedMessage> receivedMessagesRepo;

        public MessagesService(IDeletableEntityRepository<ApplicationUser> usersRepo,
            IDeletableEntityRepository<SentMessage> sentMessagesRepo, 
            IDeletableEntityRepository<ReceivedMessage> receivedMessagesRepo)
        {
            this.usersRepo = usersRepo;
            this.sentMessagesRepo = sentMessagesRepo;
            this.receivedMessagesRepo = receivedMessagesRepo;
        }

        public async Task CreateSentMessageAsync(MessageViewModel input, string ownerId)
        {
            var message = new SentMessage
            {
                // To
                ApplicationUserId = ownerId,
                Text = input.Text,
                SentOn = DateTime.UtcNow,
            };

            await this.sentMessagesRepo.AddAsync(message);
            await this.sentMessagesRepo.SaveChangesAsync();
        }

        public async Task CreateReceivedMessageAsync(MessageViewModel input, string userId)
        {
            var message = new ReceivedMessage
            {
                // From
                ApplicationUserId = userId,
                Text = input.Text,
                SentOn = DateTime.UtcNow,
            };

            await this.receivedMessagesRepo.AddAsync(message);
            await this.receivedMessagesRepo.SaveChangesAsync();
        }
    }
}
