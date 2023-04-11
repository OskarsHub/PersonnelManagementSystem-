using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class AddCourseRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Credit { get; set; }

        public Guid Teacher_id { get; set; }
    }
}
