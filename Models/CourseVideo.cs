using System.ComponentModel.DataAnnotations;

namespace e_course_web.Models
{
    public class CourseVideo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Hour { get; set; }
        [Required]
        public int Minute { get; set; }
        [Required]
        public int Part { get; set; }
        [Required]
        public string VideoUrl { get; set; }
        [Required]
        public string Description { get; set; }
    }
}