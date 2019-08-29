using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;



namespace okLims.Controllers
{


    public class DesignerController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


    }
}
