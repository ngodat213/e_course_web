using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace e_course_web.Models
{
    public class Course
    {
        [JsonProperty("_id")]
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public double Price { get; set; } // Add this property
        [Required]
        public string Category { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Rating { get; set; } // Change the type to double
        [Required]
        public int Register { get; set; } // Assuming this corresponds to the "register" field in the JSON
        [Required]
        public string TeacherId { get; set; }
        [Required]
        public string CourseImage { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string Language {  get; set; }
        [Required]
        public string UpdateAt {  get; set; }
        [Required]
        public string CreatedAt {  get; set; }
        [Required]
        public List<string> Lessons { get; set; }
        [Required]
        public List<string> Feedbacks { get; set; }
    }

    public class CourseResponse
    {
        public int count { get; set; }
        public List<Course> courses { get; set; }
    }
}