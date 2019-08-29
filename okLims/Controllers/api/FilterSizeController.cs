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
    [Route("api/FilterSize")]
    public class FilterSizeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilterSizeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetFilterSize()
        {
            List<FilterSize> Items = await _context.FilterSize.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<FilterSize> payload)
        {
            FilterSize FilterSize = payload.value;
            _context.FilterSize.Add(FilterSize);
            _context.SaveChanges();
            return Ok(FilterSize);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<FilterSize> payload)
        {
            FilterSize FilterSize = payload.value;
            _context.FilterSize.Update(FilterSize);
            _context.SaveChanges();
            return Ok(FilterSize);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<FilterSize> payload)
        {
            FilterSize FilterSize = _context.FilterSize
                .Where(x => x.SizeID == (int)payload.key)
                .FirstOrDefault();
            _context.FilterSize.Remove(FilterSize);
            _context.SaveChanges();
            return Ok(FilterSize);

        }

    }
}