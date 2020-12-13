namespace BeautyAndThePet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using BeautyAndThePet.Data.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
        [ForeignKey("ApplicationUser")]
        public int Id { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public string Neighbourhood { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public virtual ApplicationUser Habitant { get; set; }
    }
}
