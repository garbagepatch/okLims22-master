using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace okLims.Areas.Identity.Pages
{
    [AllowAnonymous]
    public class ErrorModel : PageModel
    {
        public string EventId { get; set; }

        public bool ShowEventId => !string.IsNullOrEmpty(EventId);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public void OnGet()
        {
            EventId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}