using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class Quiz
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(10000)]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public Categories categories { get; set; }
        [Required]
        public ICollection<QuizLesson> Lessons { get; set; }
    }
}