using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Course
    {

        [Key]
        public Guid Course_Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Credit { get; set; }

        public Guid Teacher_Id { get; set; }
        public Teacher Teacher { get;set; }


        public ICollection<CourseRegistration> CourseRegistration { get; set; }
    }
}
