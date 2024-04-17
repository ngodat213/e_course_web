using e_course_web.Models;
using System.ComponentModel.DataAnnotations;

namespace e_course_web.ViewModels
{
    public class CourseVideoVM
    {
        // For lesson
        public CourseLesson CourseLesson { get; set; }

        // For video
        public int Part { get; set; }
        public string Title { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public IFormFile Video { get; set; }
    }
}
