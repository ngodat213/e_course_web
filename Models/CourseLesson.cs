﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class CourseLesson
    {
        [Required] 
        public string Id { get; set; }
        [Required] 
        public string Title { get; set; }
        [Required] 
        public int Lesson { get; set; }
        [Required] 
        public List<string> Videos { get; set; }
    }
}