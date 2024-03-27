using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class Contact
    {
        public string? Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Topic { get; set; }
    }
    public class ContactResponse
    {
        public int count { get; set; }
        public List<Contact> contacts { get; set; }
    }
}