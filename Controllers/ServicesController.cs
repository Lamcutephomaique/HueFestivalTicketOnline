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
using NuGet.Packaging;
using HueFestivalTicketOnline.Models;

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public ServicesController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
          if (_context.Services == null)
          {
              return NotFound();
          }
            return await _context.Services.Select(x => _mapper.Map<ServiceDto>(x)).ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
          if (_context.Services == null)
          {
              return NotFound();
          }
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }
            var response = new
            {
                type = 1,
                detail = new { 
                    serviceId = service.ServiceId,
                    title = service.Title,
                    summary = service.Summary,
                    pathimage = service.PathImage,
                    content = service.Content,
                    longtitude = service.Longtitude,
                    latitude = service.Latitude,             
                }
            };

            return Ok(response); 
        }
        // GET: api/Services/5
        [HttpGet("GetServiceBySubmenu/{idSubmenu}")]
        public async Task<ActionResult<ServiceDto>> GetServiceBySubmenu(int idSubmenu)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }           
            //get service by submenu
            var service = await _context.Services.Where(x => x.SubMenuId == idSubmenu).ToListAsync();

            if (service == null)
            {
                return NotFound();
            }
            //create list array          
            var response = new
            {
                type = idSubmenu,
                list = service.Select(x => new {
                    serviceId = x.ServiceId,
                    title = x.Title,
                    summary = x.Summary,
                    pathimage = x.PathImage,
                    longtitude = x.Longtitude,
                    latitude = x.Latitude,
                    typedata = x.TypeData,
                })
            };
            return Ok(response);
        }

        // PUT: api/Services/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, ServiceDto service)
        {
            var serviceEntity = _mapper.Map<Service>(service);
            if (id != serviceEntity.ServiceId)
            {
                return BadRequest();
            }         

            try
            {
                _context.Entry(serviceEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        [HttpPost]
        public async Task<ActionResult<ServiceDto>> PostService(ServiceDto service)
        {
          if (_context.Services == null)
          {
              return Problem("Entity set 'FestivalHueContext.Services'  is null.");
          }
            var serviceEntity = _mapper.Map<Service>(service);
            _context.Services.Add(serviceEntity);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.ServiceId }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return (_context.Services?.Any(e => e.ServiceId == id)).GetValueOrDefault();
        }
    }
}
