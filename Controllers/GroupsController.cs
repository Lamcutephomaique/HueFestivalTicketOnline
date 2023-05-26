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

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public GroupsController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups()
        {
          if (_context.Groups == null)
          {
              return NotFound();
          }
            return await _context.Groups.Select(x => _mapper.Map<GroupDto>(x)).ToListAsync();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> GetGroup(int id)
        {
          if (_context.Groups == null)
          {
              return NotFound();
          }
            var group = await _context.Groups.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return _mapper.Map<GroupDto>(group);
        }

        // PUT: api/Groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, GroupDto group)
        {
            var groupEntity = _mapper.Map<Group>(group);
            if (id != groupEntity.GroupId)
            {
                return BadRequest();
            }


            try
            {
                _context.Entry(groupEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(GroupDto group)
        {
          if (_context.Groups == null)
          {
              return Problem("Entity set 'FestivalHueContext.Groups'  is null.");
          }
            var groupEntity = _mapper.Map<Group>(group);
            _context.Groups.Add(groupEntity);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = group.GroupId }, group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (_context.Groups == null)
            {
                return NotFound();
            }
            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return (_context.Groups?.Any(e => e.GroupId == id)).GetValueOrDefault();
        }
    }
}
