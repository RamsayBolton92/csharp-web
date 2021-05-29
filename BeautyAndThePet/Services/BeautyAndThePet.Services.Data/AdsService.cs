namespace BeautyAndThePet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Mapping;
    using BeautyAndThePet.Web.ViewModels;
    using BeautyAndThePet.Web.ViewModels.Ads;

    public class AdsService : IAdsService
    {
        private readonly IDeletableEntityRepository<Ad> adsRepo;

        public AdsService(IDeletableEntityRepository<Ad> adsRepo)
        {
            this.adsRepo = adsRepo;
        }

        public async Task CreateAsync(AdInputViewModel input, string userId)
        {
            var ad = new Ad()
            {
                SentOn = DateTime.UtcNow,
                Text = input.Text,
                ApplicationUserId = userId,
            };

            await this.adsRepo.AddAsync(ad);
            await this.adsRepo.SaveChangesAsync();
        }

        public IEnumerable<AdViewModel> GetAll()
        {
            var ads = this.adsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .To<AdViewModel>().ToList();

            return ads;
        }
    }
}