using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using okLims.Data;
using okLims.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Controllers
{

    [Authorize(Roles = Pages.MainMenu.Instrument.RoleName)]
    public class InstrumentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstrumentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            Instrument salesOrder = _context.Instrument.SingleOrDefault(x => x.InstrumentId.Equals(id));

            if (salesOrder == null)
            {
                return NotFound();
            }

            return View(salesOrder);
        }
    }
}