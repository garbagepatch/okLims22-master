using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using okLims.Data;
using okLims.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace okLims.Controllers.api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/RequestCalendar")]
    public class RequestCalendar : Controller
    {
        ApplicationDbContext db;

        public RequestCalendar(ApplicationDbContext _db)
        {
            db = _db;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCalendarRequests(DateTime start, DateTime end)
        {
            List<Request> Items = await db.Request
                .Where(s => s.Start == start)
                .Where (e => e.End == end)
                .ToListAsync();
            int Count = Items.Count();

            return Ok(new { Items, Count });
        }
        private void Update(int RequestId)
        {
            try
            {
                Request Request = new Request();
                Request = db.Request
                    .Where(x => x.RequestId.Equals(RequestId))
                    .FirstOrDefault();
                if (Request != null)
                {
                    List<RequestLine> lines = new List<RequestLine>();
                    lines = db.RequestLine.Where(x => x.RequestId.Equals(RequestId)).ToList();
                    //update master data by its lines                                       
                    db.Update(Request);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Complete(int RequestId)
        {
            try
            {
                Request request = new Request();
                request = db.Request
                    .Where(x => x.RequestId.Equals(RequestId))
                    .FirstOrDefault();
                if (request != null)
                {
                    request.StateId = 3;
                    db.Update(request);
                    db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("[action]")]
        public IActionResult AddRequest([FromBody]CrudViewModel<Request> payload)
        {
            Request Request = payload.value;
            db.Request.Add(Request);
            db.SaveChanges();
      

            this.Update(Request.RequestId);

            return Ok(Request);
        }
        [HttpPost("[action]")]
        public IActionResult Detail([FromBody]CrudViewModel<Request> payload)
        {

            Request request = payload.value;

            request.StateId = 3;
            db.Request.Update(request);
            db.SaveChanges();

            return Ok(request);
        }
        [HttpPost("[action]")]
        public IActionResult UpdateRequest([FromBody]CrudViewModel<Request> payload)
        {
            Request request = payload.value;
            db.Request.Update(request);
            if (request.StateId == 3)
            {
                this.Complete(request.RequestId);
                db.SaveChanges();
                return Ok(request);
            }
            db.SaveChanges();
            return Ok(Request);


        }

        [HttpPost("[action]")]
        public IActionResult RemoveRequest([FromBody]CrudViewModel<Request> payload)
        {
            Request Request = db.Request
                .Where(x => x.RequestId == (int)payload.key)
                     .FirstOrDefault();

            db.Request.Remove(Request);

            db.SaveChanges();
            return Ok(Request);
        }



    }

}