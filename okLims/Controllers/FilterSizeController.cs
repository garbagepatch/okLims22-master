using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Controllers
{
    [Authorize(Roles = Pages.MainMenu.FilterSize.RoleName)]
    public class FilterSizeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}