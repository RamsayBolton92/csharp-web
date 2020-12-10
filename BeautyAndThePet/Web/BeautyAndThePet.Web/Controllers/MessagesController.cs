﻿namespace BeautyAndThePet.Web.Controllers
{
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Data;
    using BeautyAndThePet.Web.ViewModels.Messages;
    using BeautyAndThePet.Web.ViewModels.Pets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

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

            var messageInput = new MessageViewModel { From = user, To = petOwner, SentOn = DateTime.UtcNow };

            return this.View(messageInput);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> New(int id, MessageViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
               return this.View(input);
            }

            var pet = this.petsService.GetById<PetViewModel>(id);
            var petOwner = pet.OwnerId;

            await this.messagesService.CreateSentMessageAsync(input, petOwner);

            var user = await this.userManager.GetUserAsync(this.User);

            await this.messagesService.CreateReceivedMessageAsync(input, user.Id);

            return this.Redirect("/Pets/MyPets");
        }

        public IActionResult AllRecieved()
        {
            return this.View();
        }

        public IActionResult Sent()
        {
            return this.View();
        }

        public IActionResult Unread()
        {
            return this.View();
        }
    }
}
