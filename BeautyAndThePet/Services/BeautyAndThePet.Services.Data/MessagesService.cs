namespace BeautyAndThePet.Services.Data
{
    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Web.ViewModels.Messages;
    using System;
    using System.Collections.Generic;
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

        public async Task CreateSentMessageAsync(MessageInputViewModel input, string userId, string ownerId)
        {
            var message = new SentMessage
            {
                // To
                ApplicationUserId = userId,
                To = ownerId,
                Text = input.Text,
                SentOn = DateTime.UtcNow,
            };

            await this.sentMessagesRepo.AddAsync(message);
            await this.sentMessagesRepo.SaveChangesAsync();
        }

        public async Task CreateReceivedMessageAsync(MessageInputViewModel input, string userId, string ownerId)
        {
            var message = new ReceivedMessage
            {
                // From
                ApplicationUserId = ownerId,
                From = userId,
                Text = input.Text,
                SentOn = DateTime.UtcNow,
            };

            await this.receivedMessagesRepo.AddAsync(message);
            await this.receivedMessagesRepo.SaveChangesAsync();
        }

        public IEnumerable<MessageViewModel> GetReceivedMessages(string userId)
        {
            var messages = this.receivedMessagesRepo.All().Where(x => x.ApplicationUserId == userId)
                .Select(x => new MessageViewModel
                { 
                    From = x.From,
                    Text = x.Text,
                    SentOn = x.SentOn,
                }).ToList();

            return messages;
        }
    }
}
