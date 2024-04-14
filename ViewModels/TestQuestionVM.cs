namespace e_course_web.ViewModels
{
    public class TestQuestionVM
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int Answer { get; set; }
        public string? ImageUrl { get; set; }
        public string? PublicId { get; set; }
        public List<string> Option { get; set; }
    }
}
