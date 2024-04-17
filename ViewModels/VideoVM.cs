using e_course_web.Models;

namespace e_course_web.ViewModels
{
    public class VideoVM
    {
        public CourseVideo CourseVideo { get; set; }
        public IFormFile Video {  get; set; }
    }
}
