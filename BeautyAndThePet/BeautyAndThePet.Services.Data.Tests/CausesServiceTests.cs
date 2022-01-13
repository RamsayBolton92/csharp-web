using BeautyAndThePet.Data.Common.Repositories;
using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Services.Mapping;
using BeautyAndThePet.Web.ViewModels;
using BeautyAndThePet.Web.ViewModels.Causes;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace BeautyAndThePet.Services.Data.Tests
{
    public class CausesServiceTests
    {
        [Theory]
        [InlineData("TestUserId")]
        [InlineData("Doni")]
        [InlineData("Momchil")]
        public async Task Create_Cause_Should_Add_New_Cause_To_List(string userId)
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var owner = new ApplicationUser { Id = "abv", UserName = "marian" };

            var causeList = new List<Cause>()
            {
                new Cause { Id = 1, Title = "Save The Orphelins", Description = "pls help", Funds = 12.4, BankAccount = "DWQ23ERG", AccountOwner = "Kris Owens", 
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 2, Title = "TestCause", Description = "pls", Funds = 1.4, BankAccount = "3ERG", AccountOwner = "Atanasko",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 3, Title = "Rave", Description = "music", Funds = 104.2, BankAccount = "ERG", AccountOwner = "Awens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
            };

            var causesRepo = new Mock<IDeletableEntityRepository<Cause>>();
            causesRepo.Setup(r => r.AddAsync(It.IsAny<Cause>())).Callback((Cause cause)=> causeList.Add(cause));

            var causesService = new CausesService(causesRepo.Object);

            var model = new CreateCauseInputModel() { Title = "Get pet to a football game", Description = "My dog is a fan of MUFC", Funds = 1300.00, BankAccount = "BG4333AWQ22", 
                AccountOwner = "Amadeus M.", Creator = "marian", Images = new List<FormFile>(), StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now,
            };

            //Act
            await causesService.CreateAsync(model, userId, "dawq");

            //Assert
            var expected = 4;
            var actual = causeList.Count();

            Assert.Equal(expected, actual);
            Assert.Equal(userId, causeList.ElementAt(3).CreatorId);
            causesRepo.Verify(x => x.AddAsync(It.IsAny<Cause>()), Times.Once);
        }

        [Fact]
        public void Get_All_Should_Return_All_Causes()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var owner = new ApplicationUser { Id = "abv", UserName = "marian" };

            var causeList = new List<Cause>()
            {
                new Cause { Id = 1, Title = "Save The Orphelins", Description = "pls help", Funds = 12.4, BankAccount = "DWQ23ERG", AccountOwner = "Kris Owens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 2, Title = "TestCause", Description = "pls", Funds = 1.4, BankAccount = "3ERG", AccountOwner = "Atanasko",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 3, Title = "Rave", Description = "music", Funds = 104.2, BankAccount = "ERG", AccountOwner = "Awens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
            };

            var causesRepo = new Mock<IDeletableEntityRepository<Cause>>();
            causesRepo.Setup(r => r.AllAsNoTracking()).Returns(causeList.AsQueryable());

            var causesService = new CausesService(causesRepo.Object);

            //Act
            var causes = causesService.GetAll();

            //Assert
            var expected = 3;
            var actual = causes.Count();

            Assert.Equal(expected, actual);
            causesRepo.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        [Fact]
        public void Get_My_Causes_Should_Return_All_User_Causes()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var owner = new ApplicationUser { Id = "abv", UserName = "marian" };

            var secondOwner = new ApplicationUser { Id = "def", UserName = "Ivan" };

            var causeList = new List<Cause>()
            {
                new Cause { Id = 1, Title = "Save The Orphelins", Description = "pls help", Funds = 12.4, BankAccount = "DWQ23ERG", AccountOwner = "Kris Owens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = secondOwner, CreatorId = "def", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 2, Title = "TestCause", Description = "pls", Funds = 1.4, BankAccount = "3ERG", AccountOwner = "Atanasko",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 3, Title = "Rave", Description = "music", Funds = 104.2, BankAccount = "ERG", AccountOwner = "Awens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
            };

            var causesRepo = new Mock<IDeletableEntityRepository<Cause>>();
            causesRepo.Setup(r => r.AllAsNoTracking()).Returns(causeList.AsQueryable());

            var causesService = new CausesService(causesRepo.Object);

            //Act
            var causes = causesService.GetMyCauses("def");

            //Assert
            var expected = 1;
            var actual = causes.Count();

            Assert.Equal(expected, actual);
            causesRepo.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        [Fact]
        public async Task Update_Cause_Should_Update_Cause_Properly()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var owner = new ApplicationUser { Id = "abv", UserName = "marian" };

            var causeList = new List<Cause>()
            {
                new Cause { Id = 1, Title = "Save The Orphelins", Description = "pls help", Funds = 12.4, BankAccount = "DWQ23ERG", AccountOwner = "Kris Owens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 2, Title = "TestCause", Description = "pls", Funds = 1.4, BankAccount = "3ERG", AccountOwner = "Atanasko",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 3, Title = "Rave", Description = "music", Funds = 104.2, BankAccount = "ERG", AccountOwner = "Awens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
            };

            var causesRepo = new Mock<IDeletableEntityRepository<Cause>>();
            causesRepo.Setup(r => r.All()).Returns(causeList.AsQueryable());

            var causesService = new CausesService(causesRepo.Object);

            var model = new EditCauseInputModel() {Title = "Get pet to a football game", Description = "My dog is a fan of MUFC", Funds = 1300.00, 
                BankAccount = "BG4333AWQ22", AccountOwner = "Amadeus M.", StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now,
            };

            //Act
            await causesService.UpdateAsync(1,model);

            //Assert
            var expected = "Get pet to a football game";
            var cause = causeList.Where(c => c.Title == "Get pet to a football game").FirstOrDefault();
            var actual = cause.Title;

            Assert.Equal(expected, actual);
            causesRepo.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task Delete_Cause_Should_Delete_Cause_Properly()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var owner = new ApplicationUser { Id = "abv", UserName = "marian" };

            var causeList = new List<Cause>()
            {
                new Cause { Id = 1, Title = "Save The Orphelins", Description = "pls help", Funds = 12.4, BankAccount = "DWQ23ERG", AccountOwner = "Kris Owens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", IsDeleted = false, CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 2, Title = "TestCause", Description = "pls", Funds = 1.4, BankAccount = "3ERG", AccountOwner = "Atanasko",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", IsDeleted = false, CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
                new Cause { Id = 3, Title = "Rave", Description = "music", Funds = 104.2, BankAccount = "ERG", AccountOwner = "Awens",
                    StartOfPeriod = DateTime.Now, EndOfPeriod = DateTime.Now, Creator = owner, CreatorId = "abv", IsDeleted = false, CauseImages = new List<CauseImage>()
                    {
                        new CauseImage { Extension = "jpg" }
                    }
                },
            };

            var causesRepo = new Mock<IDeletableEntityRepository<Cause>>();
            causesRepo.Setup(r => r.All()).Returns(causeList.AsQueryable());

            var causesService = new CausesService(causesRepo.Object);

            //Act
            await causesService.DeleteAsync(1);

            //Assert
            var deletedCause = causeList.Where(c => c.Id == 1).FirstOrDefault();

            Assert.True(deletedCause.IsDeleted);
            causesRepo.Verify(x => x.All(), Times.Once);

        }
    }
}
