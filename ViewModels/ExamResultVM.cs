using e_course_web.Models;

namespace e_course_web.ViewModels
{
    public class ExamResultVM
    {
        public List<ExamAnswer> UserChoice { set; get; }
        public TestLessonVM TestLessonVM { set; get; }
        public double Score { set; get; }
        public string UserName { set; get; }
    }
}
