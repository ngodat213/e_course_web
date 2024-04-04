using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [MaxLength(10000)]
        public string Text { get; set; }
        [Required]
        [MaxLength(255)]
        public string Topic { get; set; }
    }
}