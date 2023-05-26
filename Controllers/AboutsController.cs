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
using HueFestivalTicketOnline.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
    public class AboutsController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;
        public AboutsController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Abouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AboutDto>>> GetAbouts()
        {
          if (_context.Abouts == null)
          {
              return NotFound();
          }
          var about = await _context.Abouts.ToListAsync();
            var response = new
            {
                type = 1,
                list = about.Select(x => new
                {
                    AboutId = x.AboutId,
                    Title = x.Title,
                })
            };
            return Ok(response);
        }

        // GET: api/Abouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AboutDto>> GetAbout(int id)
        {
          if (_context.Abouts == null)
          {
              return NotFound();
          }
            var about = await _context.Abouts.FindAsync(id);
            var aboutDto = _mapper.Map<AboutDto>(about);
            if (about == null)
            {
                return NotFound();
            }
            var response = new
            {
                type = 1,
                detail = new { 
                    AboutId = aboutDto.AboutId,
                    Title = aboutDto.Title,
                    Content = aboutDto.Content,
                }
            };
            return Ok(response);
        }

        // PUT: api/Abouts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbout(int id, AboutDto about)
        {
            var aboutEntity = _mapper.Map<About>(about);
            if (id != aboutEntity.AboutId)
            {
                return BadRequest();
            }
         
            try
            {
                _context.Entry(aboutEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AboutExists(id))
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

        // POST: api/Abouts
        [HttpPost]
        public async Task<ActionResult<About>> PostAbout(AboutDto about)
        {
          if (_context.Abouts == null)
          {
              return Problem("Entity set 'FestivalHueContext.Abouts'  is null.");
          }
            var aboutEntity = _mapper.Map<About>(about);
            _context.Abouts.Add(aboutEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbout", new { id = about.AboutId }, about);
        }

        // DELETE: api/Abouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            if (_context.Abouts == null)
            {
                return NotFound();
            }
            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AboutExists(int id)
        {
            return (_context.Abouts?.Any(e => e.AboutId == id)).GetValueOrDefault();
        }
    }
}
