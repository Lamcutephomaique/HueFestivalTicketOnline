using AutoMapper;
using HueFestivalTicketOnline.Dto;
using HueFestivalTicketOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Data;

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public MenusController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenus()
        {
          if (_context.Menus == null)
          {
              return NotFound();
          }
            return await _context.Menus.Select(x => _mapper.Map<MenuDto>(x)).ToListAsync();
        }
        [HttpGet("/api/GetMenusAndSubmenu")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenusAndSubmenu()
        {
            if (_context.Menus == null)
            {
                return NotFound();
            }
            var menu = await _context.Menus.ToListAsync();
            var subMenu = await _context.SubMenus.ToListAsync();
            var response = new
            {
              data = menu.Select(x => new {
                  MenuId = x.MenuId,
                  Title = x.Title,
                  PathIcon = x.PathIcon,
                  TypeData = x.TypeData,
                  //get sub menu
                    SubMenus = subMenu.Where(y => y.MenuId == x.MenuId).Select(y => new
                    {
                        SubMenuId = y.SubMenuId,
                        Title = y.Title,
                        PathIcon = y.PathIcon,
                        MenuId = y.MenuId,
                        TypeData = y.TypeData,                      
                    }).ToList()                 
                }).ToList()
            };
            return Ok(response);
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDto>> GetMenu(int id)
        {
          if (_context.Menus == null)
          {
              return NotFound();
          }
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return _mapper.Map<MenuDto>(menu);
        }
      
        // PUT: api/Menus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, MenuDto menu)
        {
            var menuEntity = _mapper.Map<Menu>(menu);
            if (id != menuEntity.MenuId)
            {
                return BadRequest();
            }       

            try
            {
                _context.Entry(menuEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST: api/Menus
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(MenuDto menu)
        {
          if (_context.Menus == null)
          {
              return Problem("Entity set 'FestivalHueContext.Menus'  is null.");
          }
            var menuEntity = _mapper.Map<Menu>(menu);
            _context.Menus.Add(menuEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenu", new { id = menu.MenuId }, menu);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            if (_context.Menus == null)
            {
                return NotFound();
            }
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return (_context.Menus?.Any(e => e.MenuId == id)).GetValueOrDefault();
        }
    }
}
