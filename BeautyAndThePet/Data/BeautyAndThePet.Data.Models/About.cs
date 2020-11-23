namespace BeautyAndThePet.Data.Models
{
    using BeautyAndThePet.Data.Common.Models;
    public class About : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Rights { get; set; }
    }
}
