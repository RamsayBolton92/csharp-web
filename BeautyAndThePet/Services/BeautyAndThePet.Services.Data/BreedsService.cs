namespace BeautyAndThePet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using BeautyAndThePet.Data.Common.Repositories;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Mapping;

    public class BreedsService : IBreedsService
    {
        private readonly IDeletableEntityRepository<Breed> breedsRepository;

        public BreedsService(IDeletableEntityRepository<Breed> breedsRepository)
        {
            this.breedsRepository = breedsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var breeds = this.breedsRepository.AllAsNoTracking()
            .To<T>().ToList();

            return breeds;
        }
    }
}