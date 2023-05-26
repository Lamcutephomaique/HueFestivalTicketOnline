using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HueFestivalTicketOnline.Models;
using HueFestivalTicketOnline.Models;
using AutoMapper;
using HueFestivalTicketOnline.Dto;

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketTypesController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public TicketTypesController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TicketTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketType>>> GetTicketTypes()
        {
          if (_context.TicketTypes == null)
          {
              return NotFound();
          }
            return await _context.TicketTypes.ToListAsync();
        }

        // GET: api/TicketTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketType>> GetTicketType(int id)
        {
          if (_context.TicketTypes == null)
          {
              return NotFound();
          }
            var ticketType = await _context.TicketTypes.FindAsync(id);

            if (ticketType == null)
            {
                return NotFound();
            }

            return ticketType;
        }

        // PUT: api/TicketTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketType(int id, TicketType ticketType)
        {
            if (id != ticketType.TicketTypeId)
            {
                return BadRequest();
            }

            _context.Entry(ticketType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketTypeExists(id))
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

        // POST: api/TicketTypes
        [HttpPost]
        public async Task<ActionResult<TicketType>> PostTicketType(TicketTypeDto ticketType)
        {
          if (_context.TicketTypes == null)
          {
              return Problem("Entity set 'FestivalHueContext.TicketTypes'  is null.");
            }
            var ticketTypeEntity = _mapper.Map<TicketType>(ticketType);
            _context.TicketTypes.Add(ticketTypeEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketType", new { id = ticketType.TicketTypeId }, ticketType);
        }

        // DELETE: api/TicketTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketType(int id)
        {
            if (_context.TicketTypes == null)
            {
                return NotFound();
            }
            var ticketType = await _context.TicketTypes.FindAsync(id);
            if (ticketType == null)
            {
                return NotFound();
            }

            _context.TicketTypes.Remove(ticketType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketTypeExists(int id)
        {
            return (_context.TicketTypes?.Any(e => e.TicketTypeId == id)).GetValueOrDefault();
        }
    }
}
