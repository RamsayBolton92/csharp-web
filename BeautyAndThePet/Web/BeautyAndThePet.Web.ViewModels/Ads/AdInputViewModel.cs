using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyAndThePet.Web.ViewModels.Ads
{
    public class AdInputViewModel
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public virtual string ApplicationUser { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(800)]
        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
