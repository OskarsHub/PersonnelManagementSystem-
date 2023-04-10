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

        [HttpGet, Authorize(Roles = "Teacher")]
        public async Task <IActionResult> GetStudents()
        {
            return Ok(await dbStudent.Students.ToListAsync());
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> AddStudent(AddStudentRequest AddStudentRequest)
        { 
        
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
