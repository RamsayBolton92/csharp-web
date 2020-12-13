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
            var petOwner = pet.OwnerId;
            var user = await this.userManager.GetUserAsync(this.User);

            await this.messagesService.CreateSentMessageAsync(input, user.Id, petOwner);
            await this.messagesService.CreateReceivedMessageAsync(input, user.Id, petOwner);

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

        public IActionResult Unread()
        {
            return this.View();
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

            return this.View(messageViewModel);
        }

        public IActionResult MessagesNav()
        {
            return this.View();
        }
    }
}
