namespace BeautyAndThePet.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UserAdminSeeder : ISeeder
    {
        public UserAdminSeeder()
        {

        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Where(x => x.Email == "fausts@abv.bg").Any())
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = "fausts@abv.bg",
                Email = "fausts@abv.bg",
                EmailConfirmed = true,
                PhoneNumber = "0988392245",
                PhoneNumberConfirmed = true,
                IsDeleted = false,
            };

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await userManager.CreateAsync(user, "ramsaybolton");
            // var user = await userManager.GetUserAsync(User);
            // .AddToRole();
        }
    }
}
