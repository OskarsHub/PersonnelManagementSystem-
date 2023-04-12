namespace Backend.Models
{
    public class AddCourseRegistrationRequest
    {
        public Guid Student_Id { get; set; }

        public Guid Course_Id { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
