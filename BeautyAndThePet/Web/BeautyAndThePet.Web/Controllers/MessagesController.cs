namespace BeautyAndThePet.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Data;
    using BeautyAndThePet.Web.ViewModels.Messages;
    using BeautyAndThePet.Web.ViewModels.Pets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MessagesController : Controller
    {
        private readonly IPetsService petsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMessagesService messagesService;

        public MessagesController(IPetsService petsService,
            UserManager<ApplicationUser> userManager,
            IMessagesService messagesService)
        {
            this.petsService = petsService;
            this.userManager = userManager;
            this.messagesService = messagesService;
        }

        [Authorize]
        public IActionResult New(int id)
        {
            var pet = this.petsService.GetById<PetViewModel>(id);

            var petOwner = pet.OwnerUserName;
            var user = this.User.Identity.Name;

            var messageInput = new MessageInputViewModel { From = user, To = petOwner, SentOn = DateTime.UtcNow };

            return this.View(messageInput);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> New(int id, MessageInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var pet = this.petsService.GetById<PetViewModel>(id);

            var owner = await this.userManager.FindByIdAsync(pet.OwnerId);
            var user = await this.userManager.GetUserAsync(this.User);

            await this.messagesService.CreateSentMessageAsync(input, user.Id, owner.UserName);
            await this.messagesService.CreateReceivedMessageAsync(input, user.UserName, pet.OwnerId);

            return this.Redirect("/Pets/MyPets");
        }

        public async Task<IActionResult> Received()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var receivedMessagesViewModel = new MessagesListViewModel() { Messages = this.messagesService.GetReceivedMessages(user.Id) };

            return this.View(receivedMessagesViewModel);
        }

        public async Task<IActionResult> Sent()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var sentMessagesViewModel = new MessagesListViewModel() { Messages = this.messagesService.GetSentMessages(user.Id) };

            return this.View(sentMessagesViewModel);
        }

        public async Task<IActionResult> Unread()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var unreadMessagesViewModel = new MessagesListViewModel() { Messages = this.messagesService.GetUnreadMessages(user.Id) };

            return this.View(unreadMessagesViewModel);
        }

        public async Task<IActionResult> ReadSentMessage(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var messageViewModel = this.messagesService.GetSingleSentMessage(id, user.Id);

            return this.View(messageViewModel);
        }

        public async Task<IActionResult> ReadReceivedMessage(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var messageViewModel = this.messagesService.GetSingleReceivedMessage(id, user.Id);

            var correctMessage = messageViewModel.Result;

            return this.View(correctMessage);
        }

        public IActionResult MessagesNav()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> NewEmpty(string to)
        {
            var user = await this.userManager.GetUserAsync(this.User);


            if (to != null)
            {
                var messageInput = new MessageInputViewModel { From = user.UserName, To = to, SentOn = DateTime.UtcNow };

                return this.View(messageInput);
            }
            else
            {
                var messageInput = new MessageInputViewModel { From = user.UserName, SentOn = DateTime.UtcNow };

                return this.View(messageInput);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewEmpty(MessageInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var receiverName = input.To;
            var receiver = await this.userManager.FindByNameAsync(receiverName);

            await this.messagesService.CreateSentMessageAsync(input, user.Id, receiverName);
            await this.messagesService.CreateReceivedMessageAsync(input, user.UserName, receiver.Id);

            return this.Redirect("/Messages/Sent");
        }
    }
}
