

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using global::okLims.Data;
    using global::okLims.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using Syncfusion.EJ2;
    using Syncfusion.EJ2.Schedule;


    namespace okLims.Controllers
    {
        public class RequestCalendarController : Controller
        {
            private readonly ApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly Services.IEmailSender _emailSender;
            private readonly ILogger _logger;
            public RequestCalendarController(ApplicationDbContext context,
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
        }
    }