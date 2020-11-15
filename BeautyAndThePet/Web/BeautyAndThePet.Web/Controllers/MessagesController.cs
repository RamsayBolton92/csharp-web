using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyAndThePet.Web.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult New()
        {
            return this.View();
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
