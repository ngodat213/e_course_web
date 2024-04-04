using System.ComponentModel.DataAnnotations;

namespace e_course_web.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool Type { get; set; }// true is blog, false is QA
        [Required]
        public string Content { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int like { get; set; }
        [Required]
        public int dislike { get; set; }
        [Required]
        public int favorite { get; set; }
    }
}