using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class QuizQuestion
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Questions { get; set; }
        [Required]
        public int Answer { get; set; }
        [Required]
        public List<string> Option { get; set; }
    }
}