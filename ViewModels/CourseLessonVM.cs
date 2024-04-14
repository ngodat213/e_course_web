using e_course_web.Models;

namespace e_course_web.ViewModels
{
    public class CourseLessonVM
    {
        public IEnumerable<CourseLesson> CourseLesson { get; set; }
        public string Title { get; set; }
        public int Lesson { get; set; }

        public CourseVM CourseVM { get; set; }
    }
}
