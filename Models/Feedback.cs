using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class Feedback
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Like { get; set; }
        [Required]
        public User User{ get; set; }
    }
}