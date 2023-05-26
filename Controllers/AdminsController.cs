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
using NuGet.Protocol;
using HueFestivalTicketOnline.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FestivalHue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;
        public IConfiguration _configuration;

        public AdminsController(HueFestivalTicketOnlineContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost("/LoginAdmin")]
        public async Task<ActionResult<Admin>> Login(string email, string password)
        {
            try
            {
                var checkemail = _context.Admins.Where(x => x.Email == email).FirstOrDefault();
                if (checkemail == null)
                {
                    return BadRequest("Email không chính xác.");
                }
                var checkpassword = checkemail.Password == password;
                if (checkpassword == false)
                {
                    return BadRequest("Mật khẩu không chính xác.");
                }
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, checkemail.AdminId.ToString()),
                    new Claim(ClaimTypes.GivenName, checkemail.AdminName.ToString()),
                    new Claim(ClaimTypes.Email, checkemail.Email.ToString()),
                    new Claim(ClaimTypes.Role, checkemail.RoleId.ToString())

                };
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwt.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                                issuer: jwt.Issuer,
                                audience: jwt.Audience,
                                claims: claims,
                                  expires: DateTime.Now.AddDays(20),
                                  signingCredentials: signIn
                                );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                return BadRequest("Đăng nhập không thành công, đã xảy ra lỗi trong quá trình đăng nhập");
            }
        }
        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDto>>> GetAdmins()
        {
            if (_context.Admins == null)
            {
                return NotFound();
            }   
            var admin = await _context.Admins.ToListAsync();
            var response = new
            {              
                admin = admin.Select(x => new { 
                    AdminId = x.AdminId,
                    AdminName = x.AdminName,
                    Email = x.Email,
                    Password = x.Password, 
                    Phone = x.Phone,
                    Address = x.Address,
                    Avatar = x.Avatar,
                    RoleId = x.RoleId,
                    RoleName = _context.Roles.Find(x.RoleId).RoleName,
                    Created_at = x.Created_at,
                    Updated_at = x.Updated_at,
                }).ToList()
            };



            return  Ok(response);
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDto>> GetAdmin(int id)
        {
            if (_context.Admins == null)
            {
                return NotFound();
            }
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return _mapper.Map<AdminDto>(admin);
        }

        // PUT: api/Admins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.AdminId)
            {
                return BadRequest();
            }

            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admins
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(AdminDto admin)
        {
            if (admin == null)
            {
                return Problem("Entity set 'FestivalHueContext.Admins'  is null.");
            }
            _context.Admins.Add(_mapper.Map<Admin>(admin));

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = admin.AdminId }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            if (_context.Admins == null)
            {
                return NotFound();
            }
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return (_context.Admins?.Any(e => e.AdminId == id)).GetValueOrDefault();
        }
    }
}
