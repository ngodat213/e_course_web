using e_course_web.Models;

namespace e_course_web.ViewModels
{
    public class CourseDetailVM
    {
        public bool IsRegister {  get; set; }
        public Course Course { get; set; }
        public string TeacherName {  get; set; }
    }
}
