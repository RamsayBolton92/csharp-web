using BeautyAndThePet.Data.Models;
using BeautyAndThePet.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyAndThePet.Services.Data
{
    public interface IUsersService
    {
        UserInfoViewModel GetUserInfo(ApplicationUser user);
    }
}

