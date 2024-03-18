using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace e_course_web.Models
{
    public class CourseVideo 
    {
        [Required] 
        public string Id { get; set; }
        [Required] 
        public string Title { get; set; }
        [Required] 
        public int Selection { get; set; }
        [Required] 
        public int Hour { get; set; }
        [Required] 
        public int Minute { get; set; }
        [Required] 
        public int Part { get; set; }
        [Required] 
        public string VideoUrl { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required] 
        public List<string> Comments { get; set; }
    }
}