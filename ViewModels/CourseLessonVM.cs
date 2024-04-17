using e_course_web.Models;

namespace e_course_web.ViewModels
{
    public class CourseLessonVM
    {
        public string Title { get; set; }
        public int Lesson { get; set; }

        public IFormFile CourseImage { get; set; }
        public IFormFile IntroductVideo { get; set; }

        public Course Course { get; set; }
    }
}
