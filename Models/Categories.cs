using System.ComponentModel.DataAnnotations;

namespace e_course_web.Models
{
    public class Categories
    {
        [Key]
        [Required]
        public int Id {  get; set; }
        public int Category { get; set; }
    }
}
