using System.ComponentModel.DataAnnotations;

namespace e_course_web.ViewModels
{
    public class QuestionVM
    {
        [Required]
        [MaxLength(10000)]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
