namespace Backend.Models
{
    public class Course
    {
        public Guid Id { get; set; }

        public string First_name { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Credit { get; set; }


        public string Teacher_id { get; set; }
        public Teacher Teacher { get;set; }
    }
}
