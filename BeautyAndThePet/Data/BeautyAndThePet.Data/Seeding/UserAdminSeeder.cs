using BeautyAndThePet.Common;
using BeautyAndThePet.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyAndThePet.Data.Seeding
{
    public class UserAdminSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserAdminSeeder()
        {

        }

        public UserAdminSeeder(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Where(x => x.Email == "fausts@abv.bg").Any())
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = "RamsayBolton",
                Email = "fausts@abv.bg",
                EmailConfirmed = true,
                PhoneNumber = "0988392245",
                PhoneNumberConfirmed = true,
                IsDeleted = false,

            };

            await this.userManager.CreateAsync(user, "ramsaybolton");

            await dbContext.Users.AddAsync(new ApplicationUser {UserName = "RamsayBolton"});
            await dbContext.SaveChangesAsync();
        }
    }
}
