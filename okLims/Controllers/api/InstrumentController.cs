using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using okLims.Data;
using okLims.Models;
using okLims.Services;

namespace okLims.Controllers.api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Instrument")]
    public class InstrumentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;
        private readonly IEmailSender _emailSender;


        public InstrumentController(ApplicationDbContext context,
                        INumberSequence numberSequence, IEmailSender emailSender)
        {
            _context = context;
            _numberSequence = numberSequence;
            _emailSender = emailSender;

        }
        // GET: api/Instrument
        [HttpGet]

        public async Task<IActionResult> GetInstrument()
        {
            List<Instrument> Items = await _context.Instrument.ToListAsync();
            int Count = Items.Count();

            return Ok(new { Items, Count });
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Instrument result = await _context.Instrument
                .Where(x => x.InstrumentId.Equals(id))
                .Include(x => x.InstrumentLines)
                .FirstOrDefaultAsync();
            return Ok(result);
        }

        private void UpdateInstrument(int InstrumentId)
        {
            try
            {
                Instrument Instrument = new Instrument();
                Instrument = _context.Instrument
                    .Where(x => x.InstrumentId.Equals(InstrumentId))
                    .FirstOrDefault();
                if (Instrument != null)
                {
                    List<InstrumentLine> lines = new List<InstrumentLine>();
                   lines = _context.InstrumentLine.Where(x => x.InstrumentId.Equals(InstrumentId)).ToList();
                   // update master data by its lines                                       
                    _context.Update(Instrument);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("[action]")]
        public  IActionResult Insert([FromBody]CrudViewModel<Instrument> payload)
        {
            Instrument Instrument = payload.value;
            _context.Instrument.Add(Instrument);
            _context.SaveChanges();

          
            this.UpdateInstrument(Instrument.InstrumentId);

            return Ok(Instrument);
        }
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Instrument> payload)
        {
            Instrument Instrument = payload.value;
            _context.Instrument.Update(Instrument);
            _context.SaveChanges();
            return Ok(Instrument);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Instrument> payload)
        {
            Instrument Instrument = _context.Instrument
                .Where(x => x.InstrumentId == (int)payload.key)
                .FirstOrDefault();
   
            _context.Instrument.Remove(Instrument);

            _context.SaveChanges();
            return Ok(Instrument);
        }



    }

}