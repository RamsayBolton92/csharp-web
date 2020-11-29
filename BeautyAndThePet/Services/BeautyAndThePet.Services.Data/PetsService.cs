namespace BeautyAndThePet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Web.ViewModels.Pets;

    public class PetsService : IPetsService
    {
        private readonly IDeletableEntityRepository<Breed> breedsRepo;
        private readonly IDeletableEntityRepository<Pet> petsRepo;

        public PetsService(IDeletableEntityRepository<Breed> breedsRepo, IDeletableEntityRepository<Pet> petsRepo)
        {
            this.breedsRepo = breedsRepo;
            this.petsRepo = petsRepo;
        }

        public async Task CreateAsync(CreatePetInputModel input, string userId)
        {
            var pet = new Pet()
            {
                Name = input.Name,
                Sex = input.Sex,
                TypeOfPet = input.TypeOfPet,
                Breed = this.breedsRepo.All().FirstOrDefault(x => x.Name == input.Breed),
                BirthDate = input.BirthDate,
                SexualStimulus = new SexualStimulus() { Start = input.AvailableFrom, End = input.AvailableTo },
                Description = input.Description,
                OwnerId = userId,
            };

            await this.petsRepo.AddAsync(pet);
            await this.petsRepo.SaveChangesAsync();
        }
    }
}
