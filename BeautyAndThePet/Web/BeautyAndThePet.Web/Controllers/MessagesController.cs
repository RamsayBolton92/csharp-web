namespace BeautyAndThePet.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
