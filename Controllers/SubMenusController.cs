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
    public class SubMenusController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public SubMenusController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/SubMenus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubMenuDto>>> GetSubMenus()
        {
          if (_context.SubMenus == null)
          {
              return NotFound();
          }
            return await _context.SubMenus.Select(x => _mapper.Map<SubMenuDto>(x)).ToListAsync();
        }

        // GET: api/SubMenus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubMenuDto>> GetSubMenu(int id)
        {
          if (_context.SubMenus == null)
          {
              return NotFound();
          }
            var subMenu = await _context.SubMenus.FindAsync(id);

            if (subMenu == null)
            {
                return NotFound();
            }

            return _mapper.Map<SubMenuDto>(subMenu);
        }

        // PUT: api/SubMenus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubMenu(int id, SubMenuDto subMenu)
        {
            var subMenuEntity = _mapper.Map<SubMenu>(subMenu);
            if (id != subMenuEntity.SubMenuId)
            {
                return BadRequest();
            }
        
            try
            {
                _context.Entry(subMenuEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubMenuExists(id))
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

        // POST: api/SubMenus
        [HttpPost]
        public async Task<ActionResult<SubMenu>> PostSubMenu(SubMenuDto subMenu)
        {
          if (_context.SubMenus == null)
          {
              return Problem("Entity set 'FestivalHueContext.SubMenus'  is null.");
          }
            var subMenuEntity = _mapper.Map<SubMenu>(subMenu);
            _context.SubMenus.Add(subMenuEntity);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubMenu", new { id = subMenu.SubMenuId }, subMenu);
        }

        // DELETE: api/SubMenus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubMenu(int id)
        {
            if (_context.SubMenus == null)
            {
                return NotFound();
            }
            var subMenu = await _context.SubMenus.FindAsync(id);
            if (subMenu == null)
            {
                return NotFound();
            }

            _context.SubMenus.Remove(subMenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubMenuExists(int id)
        {
            return (_context.SubMenus?.Any(e => e.SubMenuId == id)).GetValueOrDefault();
        }
    }
}
