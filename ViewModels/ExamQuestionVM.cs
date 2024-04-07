using e_course_web.Models;

namespace e_course_web.ViewModels
{
    public class ExamQuestionVM
    {
        public ExamLesson QuestionLesson { get; set; }
        public IEnumerable<ExamQuestion> QuestionTest { get; set; }

        public string Question { get; set; }
        public int Answer { get; set; }
        public string Option1 {  get; set; }
        public string Option2 { get; set; }
        public string Option3 {  get; set; }
        public string Option4 { get; set; }
        public IFormFile Image { get; set; }
    }
}
