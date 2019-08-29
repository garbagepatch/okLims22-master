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
    [Route("api/ControllerType")]
    public class ControllerTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ControllerTypeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetControllerType()
        {
            List<ControllerType> Items = await _context.ControllerType.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<ControllerType> payload)
        {
            ControllerType ControllerType = payload.value;
            _context.ControllerType.Add(ControllerType);
            _context.SaveChanges();
            return Ok(ControllerType);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<ControllerType> payload)
        {
            ControllerType ControllerType = payload.value;
            _context.ControllerType.Update(ControllerType);
            _context.SaveChanges();
            return Ok(ControllerType);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<ControllerType> payload)
        {
            ControllerType ControllerType = _context.ControllerType
                .Where(x => x.ControllerID == (int)payload.key)
                .FirstOrDefault();
            _context.ControllerType.Remove(ControllerType);
            _context.SaveChanges();
            return Ok(ControllerType);

        }

    }
}