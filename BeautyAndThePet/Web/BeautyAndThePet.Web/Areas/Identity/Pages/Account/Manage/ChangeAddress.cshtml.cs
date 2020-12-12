using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeautyAndThePet.Data.Common.Repositories;
using BeautyAndThePet.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace BeautyAndThePet.Web.Areas.Identity.Pages.Account.Manage
{
    public class ChangeAddressModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly IDeletableEntityRepository<Address> addressRepo;

        public ChangeAddressModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ChangePasswordModel> logger,
            IDeletableEntityRepository<Address> addressRepo)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.addressRepo = addressRepo;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [MinLength(2)]
            [MaxLength(50)]
            [Display(Name = "New country")]
            public string NewCountry { get; set; }

            [Required]
            [MinLength(2)]
            [MaxLength(50)]
            [Display(Name = "New town")]
            public string NewTown { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var address = this.addressRepo.All().Where(x => x.Id == user.AddressId).FirstOrDefault();

            address.Country = Input.NewCountry;
            address.Town = Input.NewTown;

            await this.addressRepo.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
