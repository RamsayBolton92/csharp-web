using BeautyAndThePet.Data.Common.Repositories;
using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Services.Mapping;
using BeautyAndThePet.Web.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace BeautyAndThePet.Services.Data.Tests
{
    public class UsersServiceTests
    {
        [Fact]
        public void Get_User_Info_Should_Return_Info_Correctly()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var addressList = new List<Address>
            {
                new Address { Id = 1, Country = "a", Town = "b", },
                new Address { Id = 2, Country = "c", Town = "d", }
            };

            var breedsList = new List<Breed>
            {
                new Breed { Id = 1, Name = "Chiuaua" },
                new Breed { Id = 2, Name = "Putbull" },
                new Breed { Id = 3, Name = "Koche" }
            };

            var petList = new List<Pet>()
            {
                new Pet { Id = 2135, Name = "Pesho", OwnerId = "abv", BreedId = 1, PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } } },
                new Pet { Id = 21, Name = "Rashko", OwnerId = "def", BreedId = 2, PetImages= new List<PetImage>() { new PetImage { Extension = "jpg" } } }
            };

            var causesList = new List<Cause>()
            {
                new Cause { Id = 1, Title = "Save The Orphelins", CreatorId = "abv",Description = "pls help", Funds = 12.4, BankAccount = "DWQ23ERG", AccountOwner = "Kris Owens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 2, Title = "TestCause", CreatorId = "def",Description = "pls", Funds = 1.4, BankAccount = "3ERG", AccountOwner = "Atanasko",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 3, Title = "Rave", CreatorId = "hug" ,Description = "music", Funds = 104.2, BankAccount = "ERG", AccountOwner = "Awens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
            };

            var adsList = new List<Ad>()
            {
                new Ad{Id = 1, ApplicationUserId = "abv",Text = "aaa", SentOn = DateTime.Now },
                new Ad{Id = 2, ApplicationUserId = "def",Text = "bbb", SentOn = DateTime.Now },
                new Ad{Id=3, ApplicationUserId = "hug", Text = "vvv", SentOn = DateTime.Now }
            };


            var user = new ApplicationUser { Id = "abv", UserName = "marian", AddressId = 1 };

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.AllAsNoTracking()).Returns(petList.AsQueryable());

            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();
            breedsRepo.Setup(r => r.AllAsNoTracking()).Returns(breedsList.AsQueryable());

            var addressRepo = new Mock<IDeletableEntityRepository<Address>>();
            addressRepo.Setup(r => r.AllAsNoTracking()).Returns(addressList.AsQueryable());

            var adsRepo = new Mock<IDeletableEntityRepository<Ad>>();
            adsRepo.Setup(r => r.AllAsNoTracking()).Returns(adsList.AsQueryable());

            var causesRepo = new Mock<IDeletableEntityRepository<Cause>>();
            causesRepo.Setup(r => r.AllAsNoTracking()).Returns(causesList.AsQueryable());

            IUsersService usersService = new UsersService(breedsRepo.Object, petsRepo.Object, addressRepo.Object, adsRepo.Object, causesRepo.Object);

            //Act
            var userViewModel = usersService.GetUserInfo(user);

            //Assert
            var expected = "marian";
            var actual = userViewModel.Username;

            Assert.Equal(expected, actual);

            var expectedCount = 1;
            var actualCount = userViewModel.PetsCount;

            Assert.Equal(expectedCount, actualCount);

            expectedCount = 1;
            actualCount = userViewModel.AdsCount;

            Assert.Equal(expectedCount, actualCount);

            expectedCount = 1;
            actualCount = userViewModel.CausesCount;

            Assert.Equal(expectedCount, actualCount);
        }
    }
}
