using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using okLims.Data;
using okLims.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
[Produces("application/json")]
[Route("api/RequestLine")]
public class RequestLineController : Controller
{
    private readonly ApplicationDbContext _context;

    public RequestLineController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Request Line
    [HttpGet]
    public async Task<IActionResult> GetRequestLine()
    {
        var headers = Request.Headers["RequestId"];
        int RequestId = Convert.ToInt32(headers);
        List<RequestLine> Items = await _context.RequestLine
            .Where(x => x.RequestId.Equals(RequestId))
            .ToListAsync();
        int Count = Items.Count();
        return Ok(new { Items, Count });
    }

    private void UpdateRequest (int RequestId)
    {
        try
        {
            Request  Request  = new Request ();
            Request  = _context.Request 
                .Where(x => x.RequestId.Equals(RequestId))
                .FirstOrDefault();

            if (Request  != null)
            {
                List<RequestLine> lines = new List<RequestLine>();
                lines = _context.RequestLine.Where(x => x.RequestId.Equals(RequestId)).ToList();

      

                _context.Update(Request );

                _context.SaveChanges();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost("[action]")]
    public IActionResult Insert([FromBody]CrudViewModel<RequestLine> payload)
    {
        RequestLine RequestLine = payload.value;
      
        _context.RequestLine.Add(RequestLine);
        _context.SaveChanges();
        this.UpdateRequest (RequestLine.RequestId);
        return Ok(RequestLine);
    }

    [HttpPost("[action]")]
    public IActionResult Update([FromBody]CrudViewModel<RequestLine> payload)
    {
        RequestLine RequestLine = payload.value;
      
        _context.RequestLine.Update(RequestLine);
        _context.SaveChanges();
        this.UpdateRequest (RequestLine.RequestId);
        return Ok(RequestLine);
    }

    [HttpPost("[action]")]
    public IActionResult Remove([FromBody]CrudViewModel<RequestLine> payload)
    {
        RequestLine RequestLine = _context.RequestLine
            .Where(x => x.RequestLineId == (int)payload.key)
            .FirstOrDefault();
        _context.RequestLine.Remove(RequestLine);
        _context.SaveChanges();
        this.UpdateRequest (RequestLine.RequestId);
        return Ok(RequestLine);

    }
}
