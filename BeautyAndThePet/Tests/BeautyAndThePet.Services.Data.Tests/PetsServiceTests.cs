namespace BeautyAndThePet.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Data.Models.Enumerations;
    using BeautyAndThePet.Services.Mapping;
    using BeautyAndThePet.Web.ViewModels;
    using BeautyAndThePet.Web.ViewModels.Pets;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class PetsServiceTests
    {
        public PetsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(Assembly.Load("BeautyAndThePet.Services.Data.Tests"));
        }

        [Fact]
        public async Task PetsRepoShouldAddNewPet()
        {
            List<Breed> GetAllBreeds()
            {
                return new List<Breed>
                {
                    new Breed
                    {
                        Name = "Roshava",
                    },
                    new Breed
                    {
                        Name = "Gyrbava",
                    },
                };
            }

            List<Pet> pets = new List<Pet>
            {
                 new Pet
                    {
                        Name = "Pesho",
                    },
                 new Pet
                    {
                        Name = "Vulkan",
                    },
            };

            var pet = new Pet
            {
                Name = "Test",
            };

            async Task Add(Pet pet)
            {
                pets.Add(pet);
            }

            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.AddAsync(pet)).Returns(Add(pet));
            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();
            breedsRepo.Setup(r => r.All()).Returns(GetAllBreeds().AsQueryable());

            var service = new PetsService(breedsRepo.Object, petsRepo.Object);
            var images = new List<IFormFile>();
            var model = new CreatePetInputModel { Name = "Test", Images = images };

            await service.CreateAsync(model, "123", "abcdf");

            var petsCount = pets.Count();
            var newPetName = pets.FirstOrDefault(x => x.Name == "Test");
            Assert.Equal(3, petsCount);

            Assert.NotNull(newPetName);
            Assert.Equal("Test", newPetName.Name);
        }

        [Fact]
        public void PetsRepoReturnAllPets()
        {
            //Address
            var addressList = new List<Address> { new Address { Id = 1, Town = "pokipsi" } };
            List<Address> GetAllAddresses()
            {
                return addressList;
            }
            var addressRepo = new Mock<IDeletableEntityRepository<Address>>();
            addressRepo.Setup(r => r.AllAsNoTracking()).Returns(GetAllAddresses().AsQueryable());
            var address = addressRepo.Object.AllAsNoTracking().FirstOrDefault();
            //Images
            var images = new List<Image>();
            //Breed
            var breedsList = new List<Breed> { new Breed { Id = 1, Name = "pokipsi" } };
            List<Breed> GetAllBreeds()
            {
                return breedsList;
            }
            var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();
            breedsRepo.Setup(r => r.AllAsNoTracking()).Returns(GetAllBreeds().AsQueryable());
            var breed = breedsRepo.Object.AllAsNoTracking().FirstOrDefault();
            //Owner
            var ownersList = new List<ApplicationUser> { new ApplicationUser { Id = "dada", UserName = "dadaw", AddressId = 1 } };
            List<ApplicationUser> GetAllUsers()
            {
                return ownersList;
            }
            var ownersRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            ownersRepo.Setup(r => r.AllAsNoTracking()).Returns(GetAllUsers().AsQueryable());
            var owner = ownersRepo.Object.AllAsNoTracking().FirstOrDefault();

            var petList = new List<Pet>
                {
                    new Pet
                    {
                        Id = 1,
                        Name = "Pesho",
                        TypeOfPet = TypeOfPet.Dog,
                        Images = images,
                        Breed = breed,
                        BreedId = 1,
                        Sex = Sex.Male,
                        StartOfPeriod = DateTime.UtcNow,
                        EndOfPeriod = DateTime.UtcNow,
                        BirthDate = DateTime.UtcNow,
                        OwnerId = "dada",
                        Owner = owner,
                        Description = "qwqd",

        //public int Id { get; set; }

        //public string Name { get; set; }

        //public string TypeOfPet { get; set; }

        //public string BreedName { get; set; }

        //public string Sex { get; set; }

        //public DateTime StartOfPeriod { get; set; }

        //public DateTime EndOfPeriod { get; set; }

        //public DateTime BirthDate { get; set; }

        //public string Description { get; set; }

        //public string OwnerId { get; set; }

        //public string OwnerUserName { get; set; }

        //public string OwnerAddressTown { get; set; }

        //public string ImageUrl { get; set; }

        //public virtual ICollection<Image> Images { get; set; }
                    },
                };

            List<Pet> GetAllPets()
            {
                return petList;
            }


            var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepo.Setup(r => r.AllAsNoTracking()).Returns(GetAllPets().AsQueryable());

            var service = new PetsService(breedsRepo.Object, petsRepo.Object);
            var pets = service.GetAll(1, 10);

            Assert.Equal(2, pets.Count());
            //var model = new CustomerViewModel { Email = "Test@gmail.com", FirstName = "Test", LastName = "Testov", PhoneNumber = "123451234" };

            //await service.CreateOrUpdateCustomerAsync(model);

            //var dbCustomer = context.Customers.FirstOrDefault();

            //Assert.NotNull(dbCustomer);
            //Assert.Equal("Test@gmail.com", dbCustomer.Email);
            //Assert.Equal("Test", dbCustomer.FirstName);
            //Assert.Equal("Testov", dbCustomer.LastName);
            //Assert.Equal("123451234", dbCustomer.PhoneNumber);
        }

        //[Fact]
        //public async Task GetAllShouldGetSameCount()
        //{
        //    var breedsList = new List<Breed>();

        //    var petsList = new List<Pet>();

        //    var breedsRepo = new Mock<IDeletableEntityRepository<Breed>>();
        //    breedsRepo.Setup(x => x.All()).Returns(breedsList.AsQueryable());
        //    breedsRepo.Setup(x => x.AddAsync(It.IsAny<Breed>())).Callback((Breed breed) => breedsList.Add(breed));

        //    var petsRepo = new Mock<IDeletableEntityRepository<Pet>>();
        //    petsRepo.Setup(x => x.All()).Returns(petsList.AsQueryable());
        //    petsRepo.Setup(x => x.AddAsync(It.IsAny<Pet>())).Callback((Pet pet) => petsList.Add(pet));

        //    var service = new PetsService(breedsRepo.Object, petsRepo.Object);

        //    petsList.Add(new Pet() { Id = 1, Name = "Sofia" });
        //    petsList.Add(new Pet() { Id = 2, Name = "Pld" });

        //    var result = service.GetAll(1);



        //    Assert.Equal(2, result.Count());
        //}
    }
}
