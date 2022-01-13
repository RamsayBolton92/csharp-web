using BeautyAndThePet.Data.Common.Repositories;
using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Services.Mapping;
using BeautyAndThePet.Web.ViewModels;
using BeautyAndThePet.Web.ViewModels.Messages;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace BeautyAndThePet.Services.Data.Tests
{
    public class MessagesServiceTests
    {
        [Theory]
        [InlineData("TestUserId","TestReceiver")]
        [InlineData("Kondio","Alen")]
        [InlineData("Magapasa","Mario")]
        public async Task Create_Sent_Message_Should_Add_New_Message_To_Sent_Messages_List_Properly(string userId, string receiver)
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var user = new ApplicationUser { Id = "abv", UserName = "marian" };

            var messagesList = new List<SentMessage>()
            {
                new SentMessage{ Id = 1, ApplicationUser = user, ApplicationUserId = "abv", To = "Pesho", Text = "iskam 20 lv", SentOn = DateTime.Now},
                new SentMessage{Id = 2, ApplicationUser = user, ApplicationUserId = "abv", To = "Gosho", Text = "10 lv do ponedelnik", SentOn = DateTime.Now}
            };

            var receivedMessagesRepo = new Mock<IDeletableEntityRepository<ReceivedMessage>>();

            var sentMessagesRepo = new Mock<IDeletableEntityRepository<SentMessage>>();
            sentMessagesRepo.Setup(r => r.AddAsync(It.IsAny<SentMessage>())).Callback((SentMessage message)=>messagesList.Add(message));

            var messagesService = new MessagesService(sentMessagesRepo.Object, receivedMessagesRepo.Object);

            var model = new MessageInputViewModel { Id = 1, Text = "asl pls", SentOn = DateTime.Now };

            //Act
            await messagesService.CreateSentMessageAsync(model, userId, receiver);

            //Assert
            var expected = 3;
            var actual = messagesList.Count();

            Assert.Equal(expected, actual);
            Assert.Equal(userId, messagesList.ElementAt(2).ApplicationUserId);
            Assert.Equal(receiver, messagesList.ElementAt(2).To);
            sentMessagesRepo.Verify(x => x.AddAsync(It.IsAny<SentMessage>()), Times.Once);
        }

        [Fact]
        public void Get_Received_Messages_Should_Return_All_Received_Messages_Properly()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var user = new ApplicationUser { Id = "abv", UserName = "marian" };
            var secondUser = new ApplicationUser { Id = "def", UserName = "Pesho" };

            var messagesList = new List<ReceivedMessage>()
            {
                new ReceivedMessage{ Id = 1, ApplicationUser = user, ApplicationUserId = "abv", From = "Pesho", Text = "iskam 20 lv", IsRead = false, SentOn = DateTime.Now},
                new ReceivedMessage{Id = 2, ApplicationUser = user, ApplicationUserId = "abv", From = "Gosho", Text = "10 lv do ponedelnik", IsRead = false, SentOn = DateTime.Now},
                new ReceivedMessage{Id = 3, ApplicationUser = secondUser, ApplicationUserId = "def", From = "Sasho", Text = "koga shte mi vurnesh parite, pustinqk?", IsRead = false, SentOn = DateTime.Now}
            };

            var receivedMessagesRepo = new Mock<IDeletableEntityRepository<ReceivedMessage>>();
           receivedMessagesRepo.Setup(r => r.All()).Returns(messagesList.AsQueryable());

            var sentMessagesRepo = new Mock<IDeletableEntityRepository<SentMessage>>();

            var messagesService = new MessagesService(sentMessagesRepo.Object, receivedMessagesRepo.Object);

            //Act
            var userReceivedMessages = messagesService.GetReceivedMessages("abv");

            //Assert
            var expected = 2;
            var actual = userReceivedMessages.Count();

            Assert.Equal(expected, actual);
            receivedMessagesRepo.Verify(x => x.All(), Times.Once);
        }
    }
}
