using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class CourseComment
    {
        [Required] 
        public string Id { get; set; }
        [Required] 
        public string Title { get; set; }
        [Required] 
        public int Like { get; set; }
        [Required] 
        public List<string> UserId { get; set; }
    }
}