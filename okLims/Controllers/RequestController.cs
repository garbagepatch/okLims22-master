using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using okLims.Data;
using okLims.Extensions;
using okLims.Models;
using okLims.Services;
using Syncfusion.EJ2;
using Syncfusion.EJ2.Schedule;
using static okLims.Models.Request;

namespace okLims.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Services.IEmailSender _emailSender;
        private readonly ILogger _logger;
        public RequestController(ApplicationDbContext context,
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

            Request request = _context.Request.SingleOrDefault(x => x.EventId.Equals(id));
            if (request == null)
            {
                return NotFound();
            }

            request.StateId = 2;
            _context.Request.Update(request);
            _context.SaveChanges();
            return View(request);
        }
 
        public IActionResult RequestCalendar()
        {
    
            return View();
            
        }
        [AllowAnonymous]
 
      public IActionResult SubmitRequest()
        {
           

            return View();
         
        }

       
    }

}
    