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
    [Route("api/RequestState")]
    public class RequestStateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestStateController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRequestState()
        {
            List<RequestState> Items = await _context.RequestState.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<RequestState> payload)
        {
            RequestState RequestState = payload.value;
            _context.RequestState.Add(RequestState);
            _context.SaveChanges();
            return Ok(RequestState);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<RequestState> payload)
        {
            RequestState RequestState = payload.value;
            _context.RequestState.Update(RequestState);
            _context.SaveChanges();
            return Ok(RequestState);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<RequestState> payload)
        {
            RequestState RequestState = _context.RequestState
                .Where(x => x.StateId == (int)payload.key)
                .FirstOrDefault();
            _context.RequestState.Remove(RequestState);
            _context.SaveChanges();
            return Ok(RequestState);

        }

    }
}