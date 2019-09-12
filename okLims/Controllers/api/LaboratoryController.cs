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
    [Route("api/Laboratory")]
    public class LaboratoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LaboratoryController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetLaboratory()
        {
            List<Laboratory> Items = await _context.Laboratory.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Laboratory> payload)
        {
            Laboratory Laboratory = payload.value;
            _context.Laboratory.Add(Laboratory);
            _context.SaveChanges();
            return Ok(Laboratory);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Laboratory> payload)
        {
            Laboratory Laboratory = payload.value;
            _context.Laboratory.Update(Laboratory);
            _context.SaveChanges();
            return Ok(Laboratory);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Laboratory> payload)
        {
            Laboratory Laboratory = _context.Laboratory
                .Where(x => x.LaboratoryId == (int)payload.key)
                .FirstOrDefault();
            _context.Laboratory.Remove(Laboratory);
            _context.SaveChanges();
            return Ok(Laboratory);

        }

    }
}