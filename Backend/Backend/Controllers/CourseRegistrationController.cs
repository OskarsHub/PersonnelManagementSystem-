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
    public class CourseRegistrationController : Controller
    {
        private readonly BackendAPIDbContext dbCourseRegistration;

        public CourseRegistrationController(BackendAPIDbContext dbCourseRegistration)
        {
            this.dbCourseRegistration = dbCourseRegistration;
        }

        /// <summary>
        /// Method for getting every Course Registration
        /// </summary>
        /// <returns>List of every Course Registration</returns>
        [HttpGet, Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetCourseRegistrations()
        {
            return Ok(await dbCourseRegistration.CourseRegistrations.ToListAsync());
        }

        /// <summary>
        /// Add new Course Registration
        /// </summary>
        /// <param name="AddCourseRegistrationRequest"></param>
        /// <returns>OK</returns>
        [HttpPost, Authorize(Roles = "Student")]
        public async Task<IActionResult> AddCourseRegistration(AddCourseRegistrationRequest AddCourseRegistrationRequest)
        {

            var courseRegistration = new CourseRegistration()
            {
                CourseRegistration_Id = Guid.NewGuid(),
                Student_Id = AddCourseRegistrationRequest.Student_Id,
                Course_Id = AddCourseRegistrationRequest.Course_Id,
                RegistrationDate = AddCourseRegistrationRequest.RegistrationDate
            };

            await dbCourseRegistration.CourseRegistrations.AddAsync(courseRegistration);
            await dbCourseRegistration.SaveChangesAsync();

            return Ok();
        }

    }
}
