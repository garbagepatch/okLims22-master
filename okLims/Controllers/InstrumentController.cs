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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Services.IEmailSender _emailSender;
        private readonly ILogger _logger;
        public InstrumentController(ApplicationDbContext context,
             UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        Services.IEmailSender emailSender,
        ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;

        }
            public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            Instrument instrument = _context.Instrument.SingleOrDefault(x => x.InstrumentId.Equals(id));
            if (instrument == null)
            {
                return NotFound();

            }
            else
                return View(instrument);
        }
    }
}
