using e_course_web.Models;
using System.ComponentModel.DataAnnotations;

namespace e_course_web.ViewModels
{
    public class CourseVideoVM
    {
        public int Part { get; set; }
        public string Title { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public IFormFile Video { get; set; }
        public CourseLesson CourseLesson { get; set; }
    }
}
