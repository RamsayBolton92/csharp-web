namespace BeautyAndThePet.Services.Data
{
    using System.Collections.Generic;

    public interface IBreedsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
