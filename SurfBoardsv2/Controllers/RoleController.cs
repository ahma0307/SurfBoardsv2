using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SurfBoardsv2.Core;
using Constants = SurfBoardsv2.Core.Constants;

namespace SurfBoardsv2.Controllers
{
    public class RoleController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public IActionResult Manager()
        {
            return View();
        }
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Manager}")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
