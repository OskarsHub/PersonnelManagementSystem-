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

        /// <summary>
        /// Retrieves every teachers information
        /// </summary>
        /// <returns>information of every student</returns>
        [HttpGet]
        public async Task <IActionResult> GetTeachers()
        {
            var teacher = await dbTeacher.Teachers
                                         .Select(s => new { s.Teacher_Id, s.First_name, s.Last_name, s.Email, s.Phonenumber })
                                         .ToListAsync();

            return Ok(teacher);
        }

        /// <summary>
        /// Retrieves information for a specific teacher
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>information of specific student</returns>
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTeacher([FromRoute] Guid id)
        {
            var teacher = await dbTeacher.Teachers
                                         .Where(s => s.Teacher_Id == id)
                                         .Select(s => new { s.First_name, s.Last_name, s.Email, s.Phonenumber })
                                         .ToListAsync();

            if (teacher == null)
            {
                return BadRequest("User not found");
            }

            return Ok(teacher);
        }

        /// <summary>
        /// Registers new teacher to database
        /// </summary>
        /// <param name="AddTeacherRequest"></param>
        /// <returns>Returns bad request if email is already in use. Returns OK if registration succeeds</returns>
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> AddTeacher(AddTeacherRequest AddTeacherRequest)
        {

            var checkEmail = dbTeacher.Teachers.FirstOrDefault(s => s.Email == AddTeacherRequest.Email);

            if (checkEmail != null)
            {
                return BadRequest("Email is already in use");
            }

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
