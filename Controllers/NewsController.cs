using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HueFestivalTicketOnline.Dto;
using System.Data;
using HueFestivalTicketOnline.Models;

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public NewsController(HueFestivalTicketOnlineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/News
        [HttpGet("idPage")]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetNewss(int page = 1, int pageSize = 10)
        {
            var news = _context.Newss
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            if (news == null)
            {
                return NotFound();
            }
           
            var reposne = new
            {
                type = 1,
                list = news.Select(x => new
                {
                    newsId = x.NewsId,
                    title = x.Title,
                    parthimage = x.Image,
                    summary = x.Summary,
                    createdDate = x.Created_at,
                    updatedDate = x.Updated_at,
                })
            };
            return Ok(reposne);
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDto>> GetNews(int id)
        {
            if (_context.Newss == null)
            {
                return NotFound();
            }
            var news = await _context.Newss.FindAsync(id);
            var newsdto = _mapper.Map<NewsDto>(news);
            if (news == null)
            {
                return NotFound();
            }
            var respone = new
            {
                type = 1,
                detail = newsdto
            };

            return Ok(respone);
        }

        // PUT: api/News/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNews(int id, NewsDto news)
        {
            var newsEntity = _mapper.Map<News>(news);
            if (id != newsEntity.NewsId)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(newsEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(id))
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

        // POST: api/News
        [HttpPost]
        public async Task<ActionResult<News>> PostNews(NewsDto news)
        {
            if (_context.Newss == null)
            {
                return Problem("Entity set 'FestivalHueContext.Newss'  is null.");
            }
            var newsEntity = _mapper.Map<News>(news);
            _context.Newss.Add(newsEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNews", new { id = news.NewsId }, news);
        }

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            if (_context.Newss == null)
            {
                return NotFound();
            }
            var news = await _context.Newss.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            _context.Newss.Remove(news);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsExists(int id)
        {
            return (_context.Newss?.Any(e => e.NewsId == id)).GetValueOrDefault();
        }
    }
}
