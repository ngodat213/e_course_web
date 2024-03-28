namespace e_course_web.Models
{
    public class Blog
    {
        public bool type; // true is blog, false is QA
    }

    public class BlogResponse
    {
        public int count { get; set; }
        public List<Blog> blogs { get; set; }
    }
}