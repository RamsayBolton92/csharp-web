using BeautyAndThePet.Data.Common.Models;

namespace BeautyAndThePet.Data.Models
{
    public class About : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Rights { get; set; }
    }
}
