using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using okLims.Data;
using okLims.Models;
using okLims.Services;
using Syncfusion.EJ2.Buttons;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static okLims.Models.Request;

namespace okLims.Controllers.api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Request")]
    [Table("Request")]
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;
        private readonly IEmailSender _emailSender;
      

        public RequestController(ApplicationDbContext context,
                        INumberSequence numberSequence, IEmailSender emailSender)
        {
            _context = context;
            _numberSequence = numberSequence;
            _emailSender = emailSender;

        }
        // GET: api/Request
        // GET: api/Request
        [HttpGet]
        public async Task<IActionResult> GetRequest()
        {
            List<Request> Items = await _context.Request.ToListAsync();
            int Count = Items.Count();

            return Ok(new { Items, Count });
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Request result = await _context.Request
                .Where(x => x.RequestId.Equals(id))
                .Include(x => x.RequestLines)
                .FirstOrDefaultAsync();
            return Ok(result);
        }

        private void UpdateRequest(int RequestId)
        {
            try
            {
                Request Request = new Request();
                Request = _context.Request
                    .Where(x => x.RequestId.Equals(RequestId))
                    .FirstOrDefault();
                if (Request != null)
                {
                    List<RequestLine> lines = new List<RequestLine>();
                    lines = _context.RequestLine.Where(x => x.RequestId.Equals(RequestId)).ToList();
                    //update master data by its lines                                       
                    _context.Update(Request);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void CompleteRequest(int RequestId)
        {
            try
            {
                Request request = new Request();
                request = _context.Request
                    .Where(x => x.RequestId.Equals(RequestId))
                    .FirstOrDefault();
                if(request !=null)
                {
                    request.StateId = 3;
                    _context.Update(request);
                    _context.SaveChangesAsync();
                }
            } catch(Exception)
            {
                throw;
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]CrudViewModel<Request> payload)
        {
            Request Request = payload.value;            
            _context.Request.Add(Request);
            _context.SaveChanges();
            await _emailSender.SendEmailAsync(Request.RequesterEmail, "Order Received", "thank you");
        
            this.UpdateRequest(Request.RequestId);
           
            return Ok(Request);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Detail([FromBody]CrudViewModel<Request> payload)
        {
            
            Request request = payload.value;
       
           request.StateId = 3;
            _context.Request.Update(request);
            _context.SaveChanges();
               await _emailSender.SendEmailAsync(request.RequesterEmail, "Order Completed", "Order Number: {Requ  estId} completed on {DateTime.Now}");
            return Ok(request);
        }
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Request> payload)
        {
            Request request = payload.value;             
             _context.Request.Update(request);
        if (request.StateId == 3)
            {
                this.CompleteRequest(request.RequestId);
                _context.SaveChanges();
                return Ok(request);
            }
                _context.SaveChanges();
                return Ok(request);
            
       
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Request> payload)
        {
            Request Request = _context.Request
                .Where(x => x.RequestId == (int)payload.key)
                     .FirstOrDefault();
        
            _context.Request.Remove(Request);
             
            _context.SaveChanges();
            return Ok(Request);
        }
    


    }

}