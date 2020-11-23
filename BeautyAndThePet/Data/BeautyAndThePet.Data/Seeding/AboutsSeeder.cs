namespace BeautyAndThePet.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Common;
    using BeautyAndThePet.Data.Models;

    public class AboutsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Abouts.Any())
            {
                return;
            }

            await dbContext.Abouts.AddAsync(new About { Description = GlobalConstants.AboutUs, Rights = GlobalConstants.Rights });
            await dbContext.SaveChangesAsync();
        }
    }
}
