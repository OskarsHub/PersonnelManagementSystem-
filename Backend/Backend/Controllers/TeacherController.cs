using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TeacherController : Controller
    {

        private readonly BackendAPIDbContext dbTeacher;

        public TeacherController(BackendAPIDbContext dbTeacher)
        {
            this.dbTeacher = dbTeacher;
        }

        [HttpGet]
        public async Task <IActionResult> GetTeachers()
        {
            return Ok(await dbTeacher.Teachers.ToListAsync());
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> AddTeacher(AddTeacherRequest AddTeacherRequest)
        {

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(AddTeacherRequest.Password);

            var teacher = new Teacher()
            {
                Teacher_Id = Guid.NewGuid(),
                First_name = AddTeacherRequest.First_name,
                Last_name = AddTeacherRequest.Last_name,
                Email = AddTeacherRequest.Email,
                Phonenumber = AddTeacherRequest.Phonenumber,
                Password = passwordHash
            };

            await dbTeacher.Teachers.AddAsync(teacher);
            await dbTeacher.SaveChangesAsync();

            return Ok(teacher);
        }
    }
}
