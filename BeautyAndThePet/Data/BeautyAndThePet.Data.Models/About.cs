namespace BeautyAndThePet.Data.Models
{
    using BeautyAndThePet.Data.Common.Models;

    public class About : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public string Rights { get; set; }
    }
}
