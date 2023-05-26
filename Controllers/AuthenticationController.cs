using AutoMapper;
using HueFestivalTicketOnline.Dto;
using HueFestivalTicketOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HueFestivalTicketOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly HueFestivalTicketOnlineContext _context;
        private readonly IMapper _mapper;

        public AuthenticationController(HueFestivalTicketOnlineContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost("/dang_ky")]
        public async Task<ActionResult<User>> Register(UserDto user)
        {
            try
            {            
                var check = _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (check != null)
                {
                    return BadRequest("Email đã tồn tại.");
                }
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.RoleId = 2;
                var newUser = _mapper.Map<User>(user);            
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return Ok("Đăng ký thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Đăng ký không thành công, đã xảy ra lỗi trong quá trình đăng ký");
            }
        }

        [HttpPost("/dang_nhap")]
        public async Task<ActionResult<User>> Login(string email, string password)
        {
            try
            {
                var checkemail = _context.Users.Where(x => x.Email == email).FirstOrDefault();
                if (checkemail == null)
                {
                    return BadRequest("Email không chính xác.");
                }
                var checkpassword = BCrypt.Net.BCrypt.Verify(password, checkemail.Password);
                if (checkpassword == false)
                {
                    return BadRequest("Mật khẩu không chính xác.");
                }
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, checkemail.UserId.ToString()),
                    new Claim(ClaimTypes.GivenName, checkemail.UserName.ToString()),
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

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/changePassword/{email}")]
        public async Task<IActionResult> PutUser(string email, string password, string newpassword)
        {
            try
            {
                var checkemail = _context.Users.Where(x => x.Email == email).FirstOrDefault();
                if (checkemail == null)
                {
                    return BadRequest("Email không chính xác.");
                }
                var checkpassword = BCrypt.Net.BCrypt.Verify(password, checkemail.Password);
                if (checkpassword == false)
                {
                    return BadRequest("Mật khẩu không chính xác.");
                }
                if (newpassword == password)
                {
                    return BadRequest("Mật khẩu mới không được trùng mật khẩu cũ.");
                }
                checkemail.Password = BCrypt.Net.BCrypt.HashPassword(newpassword);
                _context.Entry(checkemail).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Đổi mật khẩu thành công");

            }
            catch (Exception ex)
            {
                return BadRequest("Đổi mật khẩu không thành công, đã xảy ra lỗi trong quá trình đổi mật khẩu");
            }
        }
    }
}
