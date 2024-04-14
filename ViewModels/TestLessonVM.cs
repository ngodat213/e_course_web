using e_course_web.Models;
using System.ComponentModel.DataAnnotations;

namespace e_course_web.ViewModels
{
    public class TestLessonVM
    {
        public int Id { get; set; }
        public int ExamId {  get; set; }
        public string Title { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Lesson { get; set; }
        public double Point { get; set; }
        public virtual List<TestQuestionVM> ExamQuestions { get; set; }
    }
}
