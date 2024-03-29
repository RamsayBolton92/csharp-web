using BeautyAndThePet.Data.Common.Repositories;
using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Services.Data;
using BeautyAndThePet.Services.Mapping;
using BeautyAndThePet.Web.ViewModels;
using BeautyAndThePet.Web.ViewModels.Pets;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace BeautyAndThePet.Services.Tests
{
    public class PetsServiceTests
    {
        [Theory]
        [InlineData("TestUserId")]
        [InlineData("aaa")]
        [InlineData("vvv")]
        public async Task Create_Pet_Should_Add_New_Pet_To_List_From_Model(string userId)
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var petList = new List<Pet>()
            {
                new Pet { Id = 2135, Name = "Pesho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false },
                new Pet { Id = 35, Name = "esho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false },
                new Pet { Id = 21, Name = "sho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false }
            };
            var breedList = new List<Breed>()
            {
                new Breed { Id = 2135, Name = "Pesho" },
                new Breed { Id = 35, Name = "esho" },
                new Breed { Id = 21, Name = "sho" }
            };

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.All()).Returns(petList.AsQueryable());
            petsRepo.Setup(r => r.AddAsync(It.IsAny<Pet>())).Callback((Pet item) => petList.Add(item));

            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();
            breedsRepo.Setup(r => r.All()).Returns(breedList.AsQueryable());

            IPetsService petsService = new PetsService(breedsRepo.Object, petsRepo.Object);

            var model = new CreatePetInputModel { Name = "Gosho", BreedId = 21, Images = new List<FormFile>() };

            //Act
            await petsService.CreateAsync(model, userId, "abvfgd");

            //Assert
            var expected = 4;
            var actual = petList.Count();

            Assert.Equal(expected, actual);
            Assert.Equal(userId, petList.ElementAt(3).OwnerId);
            petsRepo.Verify(x => x.AddAsync(It.IsAny<Pet>()), Times.Once);
            breedsRepo.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public void Pets_Count_Should_Return_Correct_Count()
        {
            //Arrange
            var petList = new List<Pet>()
            {
                new Pet { Id = 2135, Name = "Pesho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false },
                new Pet { Id = 35, Name = "esho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false },
                new Pet { Id = 21, Name = "sho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false }
            };
            var breedList = new List<Breed>()
            {
                new Breed { Id = 2135, Name = "Pesho" }, new Breed { Id = 35, Name = "esho" },
                new Breed { Id = 21, Name = "sho" }
            };

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.All()).Returns(petList.AsQueryable());

            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();

            IPetsService petsService = new PetsService(breedsRepo.Object, petsRepo.Object);

            //Act
            int count = petsService.GetCount();

            //Assert
            int expected = 3;
            Assert.Equal(expected, count);
            petsRepo.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public void Get_By_Id_Should_Return_Correct_Pet()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var address = new Address { Id = 1, Country = "a", Town = "b", };

            var owner = new ApplicationUser { Id = "awdawqe", UserName = "marian", Address = address };

            var breed = new Breed { Name = "Chiuaua", };

            var petList = new List<Pet>()
            {
                new Pet { Id = 2135, Name = "Pesho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false },
                new Pet { Id = 35, Name = "esho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false },
                new Pet { Id = 21, Name = "sho", Breed = breed, Owner = owner, PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false }
            };

            var breedList = new List<Breed>()
            {
                new Breed{ Id = 2135, Name = "Pesho" },
                new Breed { Id = 35, Name = "esho" },
                new Breed { Id = 21, Name = "sho" }
            };

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.All()).Returns(petList.AsQueryable());

            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();

            IPetsService petsService = new PetsService(breedsRepo.Object, petsRepo.Object);

            //Act
            var pet = petsService.GetById<PetViewModel>(21);

            //Assert
            var expected = pet.Name == "sho";

            Assert.True(expected);
            petsRepo.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task Delete_Pet_Should_Delete_Correct_Pet()
        {
            //Arrange
            var petList = new List<Pet>()
            {
                new Pet { Id = 2135, Name = "Pesho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false },
                new Pet { Id = 35, Name = "esho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false },
                new Pet { Id = 21, Name = "sho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false }
            };

            var breedsList = new List<Breed>()
            {
                new Breed { Id = 2135, Name = "Pesho" }, 
                new Breed { Id = 35, Name = "esho" },
                new Breed { Id = 21, Name = "sho" }
            };

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.All()).Returns(petList.AsQueryable());

            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();

            IPetsService petsService = new PetsService(breedsRepo.Object, petsRepo.Object);

            //Act
            await petsService.DeleteAsync(21);

            //Assert
            var pet = petList.Where(p => p.Id == 21).FirstOrDefault();
            var expected = pet.IsDeleted;

            Assert.True(expected);
            petsRepo.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task Update_Pet_Should_Update_Correct_Pet_With_Correct_Data()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var petList = new List<Pet>()
            {
                new Pet { Id = 2135, Name = "Pesho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false },
                new Pet { Id = 35, Name = "esho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false },
                new Pet { Id = 21, Name = "sho", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } }, IsDeleted = false }
            };
            var breedList = new List<Breed>()
            {
                new Breed { Id = 2135, Name = "Pesho" },
                new Breed { Id = 35, Name = "esho" },
                new Breed { Id = 21, Name = "sho" }
            };

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.All()).Returns(petList.AsQueryable());

            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();

            IPetsService petsService = new PetsService(breedsRepo.Object, petsRepo.Object);

            var model = new EditPetInputModel
            {
                Name = "Kotuosho",
                BreedId = 21,
            };

            //Act
            await petsService.UpdateAsync(21, model);

            //Assert
            var expected = "Kotuosho";
            var pet = petList.Where(p => p.Name == "Kotuosho").FirstOrDefault();
            var actual = pet.Name;

            Assert.Equal(expected, actual);
            petsRepo.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public void Get_My_Pets_Should_Return_All_Pets_With_Same_OwnerId()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var address = new Address { Id = 1, Country = "a", Town = "b", };

            var owner = new ApplicationUser { Id = "abv", UserName = "marian", Address = address };

            var breed = new Breed { Name = "Chiuaua" };

            var petList = new List<Pet>()
            { 
                new Pet { Id = 2135, Name = "Pesho", Breed = breed, Owner = owner, OwnerId = "abv", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } } },
                new Pet { Id = 21, Name = "Rashko", Breed = breed, Owner = owner, OwnerId = "abv", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } } }
            };

            var breedList = new List<Breed>()
            {
                new Breed { Id = 2135, Name = "Pesho" },
                new Breed { Id = 35, Name = "esho" },
                new Breed { Id = 21, Name = "sho" }
            };

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.AllAsNoTracking()).Returns(petList.AsQueryable());

            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();

            IPetsService petsService = new PetsService(breedsRepo.Object, petsRepo.Object);

            //Act
            var pets = petsService.GetMyPets("abv");

            //Assert
            var expected = 2;
            var actual = pets.Count();

            Assert.Equal(expected, actual);
            petsRepo.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        [Fact]
        public void Get_Matched_Pets_Should_Give_The_Correct_Number_Of_Matched_Pets()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var address = new Address { Id = 1, Country = "a", Town = "b" };
            var secondAddress = new Address { Id = 2, Country = "b", Town = "v" };

            var owner = new ApplicationUser { Id = "abv", UserName = "marian", Address = address };
            var secondOwner = new ApplicationUser { Id = "gde", UserName = "Ivan", Address = secondAddress };

            var breed = new Breed { Name = "Chiuaua" };

            var petList = new List<Pet>()
            {
                new Pet { Id = 2135, Name = "Pesho", Breed = breed, Owner = owner, OwnerId = "abv", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } } },
                new Pet { Id = 21, Name = "Rashko", Breed = breed, Owner = secondOwner, OwnerId = "gde", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } } }
            };

            var breedList = new List<Breed>()
            {
                new Breed { Id = 2135, Name = "Pesho" },
                new Breed { Id = 35, Name = "esho" },
                new Breed { Id = 21, Name = "sho" }
            };

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.All()).Returns(petList.AsQueryable());

            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();

            IPetsService petsService = new PetsService(breedsRepo.Object, petsRepo.Object);

            //Act
            var matchedPets = petsService.GetMatchedPets(2135, "abv");

            //Assert
            var expected = 1;
            var actual = matchedPets.Count();

            Assert.Equal(expected, actual);
            petsRepo.Verify(x => x.All(), Times.Exactly(2));
        }

        [Fact]
        public void Get_All_Should_Return_All_Pets()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var address = new Address { Id = 1, Country = "a", Town = "b", };
            var secondAddress = new Address { Id = 2, Country = "b", Town = "v", };

            var owner = new ApplicationUser { Id = "abv", UserName = "marian", Address = address };
            var secondOwner = new ApplicationUser { Id = "gde", UserName = "Ivan", Address = secondAddress };

            var breed = new Breed { Name = "Chiuaua" };

            var petList = new List<Pet>() 
            {
                new Pet { Id = 2135, Name = "Pesho", Breed = breed, Owner = owner, OwnerId = "abv", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } } },
                new Pet { Id = 21, Name = "Rashko", Breed = breed, Owner = secondOwner, OwnerId = "gde", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } } },
                new Pet { Id = 22, Name = "Opashko", Breed = breed, Owner = secondOwner, OwnerId = "gde", PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } } }
            };
            var breedList = new List<Breed>()
            {
                new Breed { Id = 2135, Name = "Pesho" },
                new Breed { Id = 35, Name = "esho" },
                new Breed { Id = 21, Name = "sho" }
            };

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.AllAsNoTracking()).Returns(petList.AsQueryable());

            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();

            IPetsService petsService = new PetsService(breedsRepo.Object, petsRepo.Object);

            //Act
            var pets = petsService.GetAll(1);

            //Assert
            var expected = 3;
            var actual = pets.Count();

            Assert.Equal(expected, actual);
            petsRepo.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

    }
}
