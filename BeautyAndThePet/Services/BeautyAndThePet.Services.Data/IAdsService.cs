using BeautyAndThePet.Web.ViewModels;
using BeautyAndThePet.Web.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyAndThePet.Services.Data
{
    public interface IAdsService
    {
        public Task CreateAsync(AdInputViewModel input, string userId);

        IEnumerable<AdViewModel> GetAll();
    }
}
