namespace Backend.Models
{
    public class AddStudentRequest
    {
        public string First_name { get; set; }

        public string Last_name { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public string Phonenumber { get; set; }

        public string Password { get; set; }
    }
}
