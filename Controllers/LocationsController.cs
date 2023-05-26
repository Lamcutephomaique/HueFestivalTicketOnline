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
using System.Data;
using HueFestivalTicketOnline.Models;

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public LocationsController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations()
        {
          if (_context.Locations == null)
          {
              return NotFound();
          }
            return await _context.Locations.Select(x => _mapper.Map<LocationDto>(x)).ToListAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetLocation(int id)
        {
          if (_context.Locations == null)
          {
              return NotFound();
          }
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return _mapper.Map<LocationDto>(location);
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationDto location)
        {
            var locationEntity = _mapper.Map<Location>(location);
            if (id != locationEntity.LocationId)
            {
                return BadRequest();
            }
        
            try
            {
                _context.Entry(locationEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(LocationDto location)
        {
          if (_context.Locations == null)
          {
              return Problem("Entity set 'FestivalHueContext.Locations'  is null.");
          }
            var locationEntity = _mapper.Map<Location>(location);
            _context.Locations.Add(locationEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.LocationId }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            if (_context.Locations == null)
            {
                return NotFound();
            }
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return (_context.Locations?.Any(e => e.LocationId == id)).GetValueOrDefault();
        }
    }
}
