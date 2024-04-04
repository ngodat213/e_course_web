using System.ComponentModel.DataAnnotations;

namespace e_course_web.Models
{
    public class Categories
    {
        [Key]
        public int Id {  get; set; }
        public string Category { get; set; }
    }
}
