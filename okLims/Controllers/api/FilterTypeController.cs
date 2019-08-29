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
    [Route("api/FilterType")]
    public class FilterTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilterTypeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetFilterType()
        {
            List<FilterType> Items = await _context.FilterType.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<FilterType> payload)
        {
            FilterType FilterType = payload.value;
            _context.FilterType.Add(FilterType);
            _context.SaveChanges();
            return Ok(FilterType);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<FilterType> payload)
        {
            FilterType FilterType = payload.value;
            _context.FilterType.Update(FilterType);
            _context.SaveChanges();
            return Ok(FilterType);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<FilterType> payload)
        {
            FilterType FilterType = _context.FilterType
                .Where(x => x.FilterID == (int)payload.key)
                .FirstOrDefault();
            _context.FilterType.Remove(FilterType);
            _context.SaveChanges();
            return Ok(FilterType);

        }

    }
}