using e_course_web.Models;

namespace e_course_web.ViewModels
{
    public class TeacherVM
    {
        public string FullName {  get; set; }
        public string Description {  get; set; }
        public string PhotoUrl { get; set; }
        public string Expert {  get; set; }
        public string Introduce {  get; set; }
        public IEnumerable<Course> Courses { get; set; } 
    }
}
