namespace BeautyAndThePet.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Data.Models.Enumerations;

    public class BreedsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Breeds.Any())
            {
                return;
            }

            await dbContext.Breeds.AddAsync(new Breed { Name = "Pincher Ninja", Characteristics = "Aggressive by default", TypeOfPet = TypeOfPet.Dog });
            await dbContext.Breeds.AddAsync(new Breed { Name = "Pitbull Terrier", Characteristics = "Dadan is king", TypeOfPet = TypeOfPet.Dog });
            await dbContext.Breeds.AddAsync(new Breed { Name = "Doggo Argentino", Characteristics = "Came from Argentina I think", TypeOfPet = TypeOfPet.Dog });
            await dbContext.Breeds.AddAsync(new Breed { Name = "Karakachan", Characteristics = "Don't touch it", TypeOfPet = TypeOfPet.Dog });
            await dbContext.Breeds.AddAsync(new Breed { Name = "Abyssinian", Characteristics = "often well balanced temperamentally and physically", TypeOfPet = TypeOfPet.Cat });
            await dbContext.Breeds.AddAsync(new Breed { Name = "American Bobtail", Characteristics = "possess a natural hunting gaze", TypeOfPet = TypeOfPet.Cat });
            await dbContext.Breeds.AddAsync(new Breed { Name = "American Curl", Characteristics = "often appear well proportioned and balanced and can vary in size.", TypeOfPet = TypeOfPet.Cat });

            await dbContext.SaveChangesAsync();
        }
    }
}
