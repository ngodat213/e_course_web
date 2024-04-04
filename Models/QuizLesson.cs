using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class QuizLesson
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Hour { get; set; }
        [Required]
        public int Minute { get; set; }
        [Required]
        public int Second { get; set; }
        [Required]
        public string Lesson { get; set; }
        [Required]
        public double Point { get; set; }
        [Required]
        public ICollection<QuizQuestion> Questions { get; set; }
    }
}