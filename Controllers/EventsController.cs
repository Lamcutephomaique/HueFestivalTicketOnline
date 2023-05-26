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
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using NuGet.Packaging;

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public EventsController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Programms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEvent()
        {
          if (_context.Events == null)
          {
              return NotFound();
          }
            return await _context.Events.Select(x => _mapper.Map<EventDto>(x)).ToListAsync();
        }

        [HttpGet("GetAllEventByDate")]
        public async Task<ActionResult<EventDto>> GetAllEventByDate()
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var Event = await _context.Events.Select(x => _mapper.Map<EventDto>(x)).ToListAsync();
            var EventDto = Event.GroupBy(x => x.Fdate).Select(x => x.ToList()).ToList();
            if (EventDto.Count == 0)
            {
                return NotFound("Không có chương trình");
            }

            var allprogrambydate = EventDto.Select(x => new
            {
                fdate = x[0].Fdate,
                count = x.Count(),
            });
            

            return Ok(allprogrambydate);
        }

        [HttpGet("GetEventByDate/{tdate}")]
        public async Task<ActionResult<EventDto>> GetEventByDate(DateTime tdate)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var Event = await _context.Events.Select(x => _mapper.Map<EventDto>(x)).ToListAsync(); ;
            var EventDto = Event.FindAll(x => x.Tdate == tdate);
            if (EventDto.Count == 0)
            {
                return NotFound("Không có chương trình");
            }
            var response = new
            {
                tdate = tdate,
                type = 0,
                detail_list = EventDto.Select(x => new {
                    tdate = x.Tdate,
                    programmId = x.EventId,
                    programmName = x.EventName,                  
                    type_inoff = x.Type_inoff,                 
                    locationId = x.LocationId,
                    LocationName = _context.Locations.Find(x.LocationId).LocationName,
                    fdate = x.Fdate,
                })
            };

            return Ok(response);
        }

        // GET: api/Programms/5
        [HttpGet("GetEventByTypeProgram/{TypeProgram}")]
        public async Task<ActionResult<EventDto>> GetEventByTypeProgram(int TypeProgram)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var Event = await _context.Events.Select(x => _mapper.Map<EventDto>(x)).ToListAsync(); ;
            var EventDto = Event.FindAll(x => x.Type_program == TypeProgram);
            if (EventDto.Count == 0)
            {
                return NotFound("Không có chương trình");
            }
            var response = new
            {
                TypeProgram = TypeProgram,
                list = EventDto.Select(x => new {
                    programmId = x.EventId,
                    programmName = x.EventName,
                    programmContent = x.EventContent,
                    type_inoff = x.Type_inoff,
                    price = x.Price,
                    type_program = x.Type_program,
                    arrange = x.arrange,
                    detail_list = new
                    {
                        fdate = x.Fdate,
                        tdate = x.Tdate,
                        locationId = x.LocationId,
                        locationName = _context.Locations.Find(x.LocationId).LocationName,
                        groupId = x.GroupId,
                        groupName = _context.Groups.Find(x.GroupId).GroupName,
                    },
                    md5 = x.Md5,
                    pathImage_list = new
                    {
                        x.PathImage,
                    }
                })              
            };

            return Ok(response);
        }

        // GET: api/Programms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEvent(int id)
        {
          if (_context.Events == null)
          {
              return NotFound();
          }
            var Event = await _context.Events.FindAsync(id);
            var EventDto = _mapper.Map<EventDto>(Event);
            if (EventDto == null)
            {
                return NotFound("Không có chương trình");
            }
            var response = new
            {
                programmId = EventDto.EventId,
                programmName = EventDto.EventName,
                programmContent = EventDto.EventContent,
                type_inoff = EventDto.Type_inoff,
                price = EventDto.Price,
                type_program = EventDto.Type_program,
                arrange = EventDto.arrange,
                detail_list = new {
                    fdate = EventDto.Fdate,
                    tdate = EventDto.Tdate,
                    locationId = EventDto.LocationId,
                    locationName = _context.Locations.Find(EventDto.LocationId).LocationName,
                    groupId = EventDto.GroupId,
                    groupName = _context.Groups.Find(EventDto.GroupId).GroupName,
                },
                md5 = EventDto.Md5,
                pathImage_list = new {
                    EventDto.PathImage,
                }

            };

            return Ok(response);
        }

        // PUT: api/Programms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, EventDto Event)
        {
            var EventEntity = _mapper.Map<Event>(Event);
            if (id != EventEntity.EventId)
            {
                return BadRequest();
            }
          
            try
            {
                _context.Entry(EventEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammExists(id))
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

        // POST: api/Programms
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(EventDto Event)
        {
          if (_context.Events == null)
          {
              return Problem("Entity set 'FestivalHueContext.Programms'  is null.");
          }
            var EventEntity = _mapper.Map<Event>(Event);
            _context.Events.Add(EventEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = Event.EventId }, Event);
        }

        // DELETE: api/Programms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramm(int id)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var Event = await _context.Events.FindAsync(id);
            if (Event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(Event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgrammExists(int id)
        {
            return (_context.Events?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
