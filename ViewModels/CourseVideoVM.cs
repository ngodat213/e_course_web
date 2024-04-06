using e_course_web.Models;
using System.ComponentModel.DataAnnotations;

namespace e_course_web.ViewModels
{
    public class CourseVideoVM
    {
        public IEnumerable<CourseVideo> CourseVideo { get; set; }
        public int Part { get; set; }
        public string Title { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public IFormFile Video { get; set; }

        public CourseLessonVM CourseLessonVM { get; set; }
    }
}
