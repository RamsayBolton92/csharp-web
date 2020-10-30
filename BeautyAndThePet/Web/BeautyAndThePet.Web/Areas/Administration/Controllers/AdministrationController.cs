namespace BeautyAndThePet.Web.Areas.Administration.Controllers
{
    using BeautyAndThePet.Common;
    using BeautyAndThePet.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
