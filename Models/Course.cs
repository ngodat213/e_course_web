using e_course_web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace e_course_web.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public Categories Category { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        [Range(0.0, 5.0)]
        public double Rating { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Register { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public string CourseImage { get; set; }

        [Required]
        [MaxLength(10)]
        public string Time { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public DateTime UpdateAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<CourseLesson> Lessons { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
