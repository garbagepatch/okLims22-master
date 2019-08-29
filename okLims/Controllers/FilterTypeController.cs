using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Controllers
{
    [Authorize(Roles = Pages.MainMenu.FilterType.RoleName)]
    public class FilterTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}