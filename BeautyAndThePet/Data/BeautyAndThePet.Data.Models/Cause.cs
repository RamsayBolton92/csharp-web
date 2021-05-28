using BeautyAndThePet.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyAndThePet.Data.Models
{
    public class Cause : BaseDeletableModel<int>
    {
        public Cause()
        {
            this.CauseImages = new HashSet<CauseImage>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Funds { get; set; }

        public string BankAccount { get; set; }

        public string AccountOwner { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual DateTime StartOfPeriod { get; set; }

        public virtual DateTime EndOfPeriod { get; set; }

        public virtual ICollection<CauseImage> CauseImages { get; set; }
    }
}
