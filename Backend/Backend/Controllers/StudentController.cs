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
    public class StudentController : Controller
    {
        private readonly BackendAPIDbContext dbStudent;

        public StudentController(BackendAPIDbContext dbStudent)
        {
            this.dbStudent = dbStudent;
        }

        /// <summary>
        /// Retrieves every students information
        /// </summary>
        /// <returns>information of every student</returns>
        [HttpGet, Authorize(Roles = "Teacher")]
        public async Task <IActionResult> GetStudents()
        {
            var students = await dbStudent.Students
                                         .Select(s => new { s.Student_Id, s.First_name, s.Last_name, s.Email, s.Phonenumber })
                                         .ToListAsync();

            return Ok(students);
        }

        /// <summary>
        /// Retrieves information for a specific student
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>information of specific student</returns>
        [HttpGet]
        [Route ("{id:guid}")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid id)
        {
            var student = await dbStudent.Students
                                         .Where(s => s.Student_Id == id)
                                         .Select(s => new {s.First_name, s.Last_name, s.Email, s.Phonenumber})
                                         .ToListAsync();

            if (student == null)
            {
                return BadRequest("User not found");
            }

            return Ok(student);
        }

        /// <summary>
        /// Registers new student to database
        /// </summary>
        /// <param name="AddStudentRequest"></param>
        /// <returns>Returns bad request if email is already in use. Returns OK if registration succeeds</returns>
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> AddStudent(AddStudentRequest AddStudentRequest)
        {

            var checkEmail = dbStudent.Students.FirstOrDefault(s => s.Email == AddStudentRequest.Email);

            if (checkEmail != null)
            {
                return BadRequest("Email is already in use");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(AddStudentRequest.Password);

            var student = new Student()
            { 
                Student_Id = Guid.NewGuid(),
                First_name = AddStudentRequest.First_name,
                Last_name = AddStudentRequest.Last_name,
                Birthday = AddStudentRequest.Birthday,
                Email = AddStudentRequest.Email,
                Phonenumber = AddStudentRequest.Phonenumber,
                Password = passwordHash
            };

            await dbStudent.Students.AddAsync(student);
            await dbStudent.SaveChangesAsync();

            return Ok(student);
        }
    }
}
