namespace BeautyAndThePet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<Pet> petsRepo;
        private readonly IDeletableEntityRepository<Breed> breedsRepo;
        private readonly IDeletableEntityRepository<Address> addressRepo;
        private readonly IDeletableEntityRepository<Ad> adsRepo;
        private readonly IDeletableEntityRepository<Cause> causesRepo;

        public UsersService(IDeletableEntityRepository<Breed> breedsRepo, IDeletableEntityRepository<Pet> petsRepo,
            IDeletableEntityRepository<Address> addressRepo, IDeletableEntityRepository<Ad> adsRepo, IDeletableEntityRepository<Cause> causesRepo)
        {
            this.petsRepo = petsRepo;
            this.breedsRepo = breedsRepo;
            this.addressRepo = addressRepo;
            this.adsRepo = adsRepo;
            this.causesRepo = causesRepo;
        }

        public UserInfoViewModel GetUserInfo(ApplicationUser user)
        {
            var address = this.addressRepo.AllAsNoTracking().Where(x => x.Id == user.AddressId).FirstOrDefault();

            var pets = this.petsRepo.AllAsNoTracking().Where(x => x.OwnerId == user.Id);
            Dictionary<int, Dictionary<string, string>> petsDictionary = new Dictionary<int, Dictionary<string, string>>();

            foreach (var pet in pets)
            {
                var breed = this.breedsRepo.AllAsNoTracking().Where(x => x.Id == pet.BreedId).FirstOrDefault();

                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add(pet.Name, breed.Name);

                petsDictionary[pet.Id] = dict;
            }

            var ads = this.adsRepo.AllAsNoTracking().Where(x => x.ApplicationUserId == user.Id);
            var adsDictionary = new Dictionary<string, string>();

            foreach (var ad in ads)
            {
                adsDictionary[ad.Text] = ad.CreatedOn.ToShortDateString();
            }

            var causes = this.causesRepo.AllAsNoTracking().Where(x => x.CreatorId == user.Id);
            var causesDictionary = new Dictionary<string, string>();

            foreach (var cause in causes)
            {
                causesDictionary[cause.Title] = cause.Description;
            }

            var userViewModel = new UserInfoViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                City = address.Town,
                Country = address.Country,
                CreatedOn = user.CreatedOn.ToShortDateString(),
                PetsCount = pets.Count(),
                AdsCount = ads.Count(),
                CausesCount = causes.Count(),
                PetsDictionary = petsDictionary,
                AdsDictionary = adsDictionary,
                CausesDictionary = causesDictionary,
            };

            return userViewModel;
        }
    }
}
