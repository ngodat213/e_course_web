namespace e_course_web.ViewModels
{
    public class ExamQuestionDetailVM
    {
        public string Question { get; set; }
        public int Answer { get; set; }
        public List<string> Option { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image {  get; set; }
    }
}
