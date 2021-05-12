using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyAndThePet.Web.ViewModels
{
    public class AdViewModel : IMapFrom<Ad>
    {


        public string ApplicationUser { get; set; }

        public string Text { get; set; }

        public DateTime SentOn { get; set; }
    }
}
