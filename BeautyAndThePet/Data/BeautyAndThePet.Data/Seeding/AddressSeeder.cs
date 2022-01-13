namespace BeautyAndThePet.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Common;
    using BeautyAndThePet.Data.Models;

    public class AddressSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Addresses.Any())
            {
                return;
            }

            await dbContext.Addresses.AddAsync(new Address { Country = "Bulgaria", Town = "Sofia" });
            await dbContext.SaveChangesAsync();
        }
    }
}
