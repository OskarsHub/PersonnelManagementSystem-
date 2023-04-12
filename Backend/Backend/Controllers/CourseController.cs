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
    public class CourseController : Controller
    {
        private readonly BackendAPIDbContext dbCourse;

        public CourseController(BackendAPIDbContext dbCourse, BackendAPIDbContext dbTeacher)
        {
            this.dbCourse = dbCourse;
        }

        /// <summary>
        /// Retrieves list of all courses
        /// </summary>
        /// <returns>list of all courses</returns>
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await dbCourse.Courses.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCourse([FromRoute] Guid id)
        {
            var course = await dbCourse.Courses.Where(s => s.Course_Id == id).ToListAsync();

            if (course == null)
            {
                return BadRequest("Course not found");
            }

            return Ok(course);
        }

        /// <summary>
        /// Method for adding new course
        /// </summary>
        /// <param name="AddCourseRequest"></param>
        /// <returns>OK</returns>
        [HttpPost, Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddCourse(AddCourseRequest AddCourseRequest)
        {

            var course = new Course()
            {
                Course_Id = Guid.NewGuid(),
                Name = AddCourseRequest.Name,
                Description = AddCourseRequest.Description,
                Credit = AddCourseRequest.Credit,
                Teacher_Id = AddCourseRequest.Teacher_id
            };

            await dbCourse.Courses.AddAsync(course);
            await dbCourse.SaveChangesAsync();

            return Ok(course);
        }
    }
}
