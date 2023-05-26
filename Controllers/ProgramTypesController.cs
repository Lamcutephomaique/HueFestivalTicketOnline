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
    public class ProgramTypesController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public ProgramTypesController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ProgramTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramTypeDto>>> GetProgramTypes()
        {
          if (_context.ProgramTypes == null)
          {
              return NotFound();
          }
            return await _context.ProgramTypes.Select(x => _mapper.Map<ProgramTypeDto>(x)).ToListAsync();
        }

        // GET: api/ProgramTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramTypeDto>> GetProgramType(int id)
        {
          if (_context.ProgramTypes == null)
          {
              return NotFound();
          }
            var programType = await _context.ProgramTypes.FindAsync(id);

            if (programType == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProgramTypeDto>(programType); ;
        }

        // PUT: api/ProgramTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramType(int id, ProgramTypeDto programType)
        {
            var programTypeEntity = _mapper.Map<ProgramType>(programType);
            if (id != programTypeEntity.Type_program)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(programTypeEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramTypeExists(id))
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

        // POST: api/ProgramTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProgramType>> PostProgramType(ProgramTypeDto programType)
        {
          if (_context.ProgramTypes == null)
          {
              return Problem("Entity set 'FestivalHueContext.ProgramTypes'  is null.");
          }
            var programTypeEntity = _mapper.Map<ProgramType>(programType);
            _context.ProgramTypes.Add(programTypeEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgramType", new { id = programType.Type_program }, programType);
        }

        // DELETE: api/ProgramTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramType(int id)
        {
            if (_context.ProgramTypes == null)
            {
                return NotFound();
            }
            var programType = await _context.ProgramTypes.FindAsync(id);
            if (programType == null)
            {
                return NotFound();
            }

            _context.ProgramTypes.Remove(programType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramTypeExists(int id)
        {
            return (_context.ProgramTypes?.Any(e => e.Type_program == id)).GetValueOrDefault();
        }
    }
}
