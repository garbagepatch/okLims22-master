using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using okLims.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Controllers
{
    [Authorize(Roles = Pages.MainMenu.ControllerType.RoleName)]
    public class ControllerTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}