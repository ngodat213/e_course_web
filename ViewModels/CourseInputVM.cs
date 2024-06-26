﻿using e_course_web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace e_course_web.ViewModels
{
    public class CourseInputVM
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public string TeacherId { get; set; }

        [Required]
        public IFormFile CourseImage { get; set; }
        [Required]
        public IFormFile IntroductVideo { get; set; }

        [Required]
        [MaxLength(10)]
        public string Time { get; set; }

        [Required]
        public string Language { get; set; }

    }
}
