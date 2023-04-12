using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class CourseRegistration
    {
        [Key]
        public Guid CourseRegistration_Id { get; set; }

        public Guid Student_Id { get; set; }
        public Student Student { get; set; }

        public Guid Course_Id { get; set; }
        public Course Course { get; set; }

        public DateTime RegistrationDate { get; set; }

    }
}
