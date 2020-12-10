using System.Collections.Generic;

namespace BeautyAndThePet.Services.Data
{
    public interface IBreedsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
