using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HueFestivalTicketOnline.Models;
using AutoMapper;
using HueFestivalTicketOnline.Dto;
using HueFestivalTicketOnline.Models;

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketLocationsController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public TicketLocationsController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TicketLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketLocationDto>>> GetTicketLocations()
        {
          if (_context.TicketLocations == null)
          {
              return NotFound();
          }
          var ticketLocations = await _context.TicketLocations.ToListAsync();
            var response = new
            {
                type = 1,
                data = ticketLocations.Select(x => _mapper.Map<TicketLocationDto>(x))
            };
            return  Ok(response);
        }

        // GET: api/TicketLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketLocation>> GetTicketLocation(int id)
        {
          if (_context.TicketLocations == null)
          {
              return NotFound();
          }
            var ticketLocation = await _context.TicketLocations.FindAsync(id);

            if (ticketLocation == null)
            {
                return NotFound();
            }

            return ticketLocation;
        }

        // PUT: api/TicketLocations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketLocation(int id, TicketLocation ticketLocation)
        {
            if (id != ticketLocation.TicketLocationId)
            {
                return BadRequest();
            }

            _context.Entry(ticketLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketLocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TicketLocations
        [HttpPost]
        public async Task<ActionResult<TicketLocation>> PostTicketLocation(TicketLocation ticketLocation)
        {
          if (_context.TicketLocations == null)
          {
              return Problem("Entity set 'FestivalHueContext.TicketLocations'  is null.");
          }
            _context.TicketLocations.Add(ticketLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketLocation", new { id = ticketLocation.TicketLocationId }, ticketLocation);
        }

        // DELETE: api/TicketLocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketLocation(int id)
        {
            if (_context.TicketLocations == null)
            {
                return NotFound();
            }
            var ticketLocation = await _context.TicketLocations.FindAsync(id);
            if (ticketLocation == null)
            {
                return NotFound();
            }

            _context.TicketLocations.Remove(ticketLocation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketLocationExists(int id)
        {
            return (_context.TicketLocations?.Any(e => e.TicketLocationId == id)).GetValueOrDefault();
        }
    }
}
