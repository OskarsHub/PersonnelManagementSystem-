using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Student
    {

        [Key]
        public Guid Student_Id { get; set; }

        public string First_name { get; set; }

        public string Last_name { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public string Phonenumber { get; set; }

        public string Password { get; set; }


        public ICollection<CourseRegistration> CourseRegistration { get; set; }
    }
}
