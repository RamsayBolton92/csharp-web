using BeautyAndThePet.Data.Common.Repositories;
using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Services.Data;
using BeautyAndThePet.Services.Mapping;
using BeautyAndThePet.Web.ViewModels;
using BeautyAndThePet.Web.ViewModels.Ads;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace BeautyAndThePet.Tests
{
    public class AdsServiceTests
    {
        [Fact]
        public async Task Create_Ad_Should_Add_New_Ad_To_List() 
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var applicationUser = new ApplicationUser { Id = "adqwe", UserName = "Marian" };

            var adsList = new List<Ad>()
            {
                new Ad { Id = 1, Text = "aaa", SentOn = DateTime.Now, ApplicationUser = applicationUser, ApplicationUserId = "adqwe"},
                new Ad {Id = 2, Text = "bbb", SentOn = DateTime.Now, ApplicationUser = applicationUser, ApplicationUserId = "adqwe"},
                new Ad  {Id = 3, Text = "vvv", SentOn = DateTime.Now, ApplicationUser = applicationUser, ApplicationUserId = "adqwe"}
            };

            var adsRepo = new Mock<IDeletableEntityRepository<Ad>>();
            adsRepo.Setup(r => r.AddAsync(It.IsAny<Ad>())).Callback((Ad ad) => adsList.Add(ad));

            var adsService = new AdsService(adsRepo.Object);

            var model = new AdInputViewModel { Id = 4, Text = "ggg", SentOn = DateTime.Now, ApplicationUser = "Marian" };

            //Act
            await adsService.CreateAsync(model, "adqwe");

            //Assert
            var expected = 4;
            var actual = adsList.Count();

            Assert.Equal(expected,actual);
            adsRepo.Verify(x => x.AddAsync(It.IsAny<Ad>()), Times.Once);
        }

        [Fact]
        public void Get_All_Ads_Should_Return_All_Ads()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var applicationUser = new ApplicationUser { Id = "adqwe", UserName = "Marian" };

            var adsList = new List<Ad>()
            {
                new Ad{Id = 1, Text = "aaa", SentOn = DateTime.Now, ApplicationUser = applicationUser, ApplicationUserId = "adqwe"},
                new Ad{Id = 2, Text = "bbb", SentOn = DateTime.Now, ApplicationUser = applicationUser, ApplicationUserId = "adqwe"},
                new Ad{Id=3, Text = "vvv", SentOn = DateTime.Now, ApplicationUser = applicationUser, ApplicationUserId = "adqwe"}
            };

            var adsRepo = new Mock<IDeletableEntityRepository<Ad>>();
            adsRepo.Setup(r => r.AllAsNoTracking()).Returns(adsList.AsQueryable());

            var adsService = new AdsService(adsRepo.Object);

            //Act
            var list = adsService.GetAll();

            //Assert
            var expected = 3;
            var actual = list.Count();

            Assert.Equal(expected,actual);
            adsRepo.Verify(x => x.AllAsNoTracking(), Times.Once);
        }
    }
}
