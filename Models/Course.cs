using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class Course
    {
        [Required] 
        public string Id { get; set; }
        [Required] 
        public string Title { get; set; }
        [Required] 
        public double Price { get; set; }
        [Required] 
        public string Category { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required] 
        public string Rating { get; set; }
        [Required] 
        public int Order { get; set; }
        [Required] 
        public string TeacherId { get; set; }
        [Required] 
        public string CourseImage { get; set; }
        [Required] 
        public string Time { get; set; }
        [Required] 
        public List<string> Lessons { get; set; }
    }

    public class CoursesResponse
    {
        public int count { get; set; }
        public List<Course> courses { get; set; }
    }
}