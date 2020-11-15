namespace BeautyAndThePet.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using BeautyAndThePet.Data;
    using BeautyAndThePet.Web.ViewModels;
    using BeautyAndThePet.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { AboutUs = this.db.Abouts.First().Description };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Actions()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
