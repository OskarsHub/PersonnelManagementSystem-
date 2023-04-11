using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly BackendAPIDbContext dbStudent;
        private readonly BackendAPIDbContext dbTeacher;

        public LoginController(IConfiguration configuration ,BackendAPIDbContext dbStudent, BackendAPIDbContext dbTeacher)
        {
            _configuration = configuration;

            this.dbStudent = dbStudent;
            this.dbTeacher = dbTeacher;
        }

        /// <summary>
        /// Login method
        /// </summary>
        /// <param name="LoginRequest"></param>
        /// <returns>Token if login was successful</returns>
        [HttpPost]
        public IActionResult Login(LoginRequest LoginRequest)
        {

        string Email = LoginRequest.Email;
        string Password = LoginRequest.Password;
        int Level = LoginRequest.Level;

            if (Level == 0)
            {
                var user = dbStudent.Students.Where(s => s.Email == Email).FirstOrDefault();

                if (user == null || !BCrypt.Net.BCrypt.Verify(Password, user.Password))
                {
                    return BadRequest("Incorrect username or password");
                }

                string token = CreateToken(user);
                return Ok(token);
            }

            if (Level == 1)
            {
                Console.WriteLine("Ennen");
                var user = dbTeacher.Teachers.Where(t => t.Email == Email).FirstOrDefault();
                Console.WriteLine("Jälkeen");

                if (user == null || !BCrypt.Net.BCrypt.Verify(Password, user.Password))
                {
                    if (user == null)
                    {
                        return BadRequest("Incorrect username or password");
                    }

                }

                string token = CreateToken(user);
                return Ok(token);

            }

            return BadRequest();

        }

        /// <summary>
        /// Creates JWT token for student
        /// </summary>
        /// <param name="user">Student</param>
        /// <returns>JWT Token</returns>
        private string CreateToken(Student user)
        {
            string Id = user.Student_Id.ToString();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Id),
                new Claim(ClaimTypes.Role, "Student"),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Creates JWT token for teacher
        /// </summary>
        /// <param name="user">Teacher</param>
        /// <returns>JWT Token/returns>
        private string CreateToken(Teacher user)
        {
            string Id = user.Teacher_Id.ToString();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Id),
                new Claim(ClaimTypes.Role, "Teacher"),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }

}

