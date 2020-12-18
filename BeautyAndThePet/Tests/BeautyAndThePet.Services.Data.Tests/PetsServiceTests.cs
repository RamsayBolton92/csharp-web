namespace BeautyAndThePet.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Data.Models.Enumerations;
    using BeautyAndThePet.Web.ViewModels.Pets;
    using Moq;
    using Xunit;

    public class PetsServiceTests
    {
        [Fact]
        public async Task GetAllShouldReturnAllPets()
        {
            var breedsList = new List<Breed>();

            var petsList = new List<Pet>();

            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();
            breedsRepo.Setup(x => x.All()).Returns(breedsList.AsQueryable());
            breedsRepo.Setup(x => x.AddAsync(It.IsAny<Breed>())).Callback((Breed breed) => breedsList.Add(breed));

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(x => x.All()).Returns(petsList.AsQueryable());
            petsRepo.Setup(x => x.AddAsync(It.IsAny<Pet>())).Callback((Pet pet) => petsList.Add(pet));

            var service = new PetsService(breedsRepo.Object, petsRepo.Object);
            service.CreateAsync();
        }
    }
}
