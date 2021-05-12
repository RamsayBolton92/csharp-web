using BeautyAndThePet.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyAndThePet.Data.Models
{
    public class Ad : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
