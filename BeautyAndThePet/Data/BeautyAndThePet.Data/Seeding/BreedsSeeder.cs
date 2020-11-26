namespace BeautyAndThePet.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Models;

    public class BreedsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Breeds.Any())
            {
                return;
            }

            await dbContext.Breeds.AddAsync(new Breed { Name = "Pincher Ninja", Characteristics = "Aggressive by default"});
            await dbContext.Breeds.AddAsync(new Breed { Name = "Pitbull Terrier", Characteristics = "Dadan is king"});
            await dbContext.Breeds.AddAsync(new Breed { Name = "Doggo Argentino", Characteristics = "Came from Argentina I think"});
            await dbContext.Breeds.AddAsync(new Breed { Name = "Karakachan", Characteristics = "Don't touch it"});
            await dbContext.SaveChangesAsync();
        }
    }
}
