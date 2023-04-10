namespace Backend.Models
{
    public class Teacher
    {

        public Guid Teacher_Id { get; set; }

        public string First_name { get; set; }

        public string Last_name { get; set; }

        public string Email { get; set; }

        public string Phonenumber { get; set; }

        public string Password { get; set; }


        public ICollection<Course> Courses { get; set; }
    }
}
