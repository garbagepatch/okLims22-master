using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using okLims.Data;
using okLims.Helpers;
using okLims.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

//namespace okLims.Controllers
//{
/*
    [Authorize]
    [Route("[controller]/[action]")]
    public class RequestCalenderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private DA _DA { get; set; }
        private readonly api.RequestController _requestController;

        public RequestCalenderController(IOptions<AppSettings> settings, ApplicationDbContext context)
        {
            _context = context;
         
           
        }
        [Authorize(Roles = Pages.MainMenu.Dashboard.RoleName)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Requests(string start, string end)
        {
            List<Request> Requests = _DA.GetCalendarRequests(start, end);

            return Json(Requests);
        }

        [HttpPost]
        public IActionResult UpdateRequest([FromBody] Request evt)
        {
            string message = String.Empty;

            message = _DA.UpdateRequest(evt);

            return Json(new { message });
        }

   /*     [HttpPost]
        public async Task<IActionResult> AddRequestAsync([FromBody] Request evt)
        {
            List<Request> Items = await _context.Request.ToListAsync();
            int Count = Items.Count();
            message = _DA.AddRequest(evt, out RequestId);

          return Json(new { message, RequestId });
        }
        */
/*        [HttpPost]
       public IActionResult DeleteRequest([FromBody] Request evt)
        {
            string message = String.Empty;

            message = _DA.DeleteRequest(evt.RequestId);

            return Json(new { message });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
*///}