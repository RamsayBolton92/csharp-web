using BeautyAndThePet.Data.Common.Repositories;
using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Services.Mapping;
using BeautyAndThePet.Web.ViewModels.Causes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyAndThePet.Services.Data
{
    public class CausesService : ICausesService
    {
        private readonly IDeletableEntityRepository<Cause> causesRepo;
        private readonly string[] allowedExtensions = new[] { "jpg", "png" };

        public CausesService(IDeletableEntityRepository<Cause> causesRepo)
        {
            this.causesRepo = causesRepo;
        }

        public async Task CreateAsync(CreateCauseInputModel input, string userId, string imagePath)
        {
            var cause = new Cause()
            {
                Title = input.Title,
                Description = input.Description,
                Funds = input.Funds,
                BankAccount = input.BankAccount,
                AccountOwner = input.AccountOwner,
                CreatorId = userId,
                StartOfPeriod = input.StartOfPeriod,
                EndOfPeriod = input.EndOfPeriod,
            };


            Directory.CreateDirectory($"{imagePath}/causes/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new CauseImage
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };

                cause.CauseImages.Add(dbImage);

                var physicalPath = $"{imagePath}/causes/{dbImage.Id}.{extension}";

                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }


            await this.causesRepo.AddAsync(cause);
            await this.causesRepo.SaveChangesAsync();
        }

        public IEnumerable<CauseViewModel> GetAll()
        {
            var causes = this.causesRepo.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .To<CauseViewModel>().ToList();

            return causes;
        }

        public T GetById<T>(int id)
        {
            var pet = this.causesRepo.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return pet;
        }

        public IEnumerable<CauseViewModel> GetMyCauses(string userId)
        {
            var causes = this.causesRepo.AllAsNoTracking()
                .Where(x => x.CreatorId == userId)
                .OrderByDescending(x => x.Id)
                .To<CauseViewModel>().ToList();

            return causes;
        }

        public async Task DeleteAsync(int id)
        {
            var cause = this.causesRepo.All().FirstOrDefault(x => x.Id == id);

            cause.IsDeleted = true;

            await this.causesRepo.SaveChangesAsync();
        }
    }
}

