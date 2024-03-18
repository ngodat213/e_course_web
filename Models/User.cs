using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class User
    {
        [EmailAddress]
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string username {get; set; }
        public string photoUrl { get; set; }
    }

    public class UserResponse
    {
        public string message { get; set; }
        public string token { get; set; }
    }
}