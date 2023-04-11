using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly BackendAPIDbContext dbCourse;
        private readonly BackendAPIDbContext dbTeacher;

        public CourseController(BackendAPIDbContext dbCourse, BackendAPIDbContext dbTeacher)
        {
            this.dbCourse = dbCourse;
            this.dbTeacher = dbTeacher;
        }
        /// <summary>
        /// Retrieves list of all courses
        /// </summary>
        /// <returns>list of all courses</returns>
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await dbCourse.Courses.ToListAsync());
        }

        /// <summary>
        /// Method for adding new course
        /// </summary>
        /// <param name="AddCourseRequest"></param>
        /// <returns>OK</returns>
        [HttpPost]
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
