using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class CourseLesson
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Lesson { get; set; }
        public List<CourseVideo> Videos { get; set; }
    }
}