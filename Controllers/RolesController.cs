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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]*/
    public class RolesController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public RolesController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            return await _context.Roles.Select(x => _mapper.Map<RoleDto>(x)).ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRole(int id)
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
          // get role by id 
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound("Role not found.");
            }
            return _mapper.Map<RoleDto>(role);

        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RoleDto role)
        {
           var roleEntity = _mapper.Map<Role>(role);
            if (id != roleEntity.RoleId)
            {
                return BadRequest();
            }
          
            try
            {
                _context.Entry(roleEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RoleDto role)
        {
          if (_context.Roles == null)
          {
              return Problem("Entity set 'FestivalHueContext.Roles'  is null.");
          }
          var roleEntity = _mapper.Map<Role>(role);
            _context.Roles.Add(roleEntity);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.RoleId }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return (_context.Roles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
