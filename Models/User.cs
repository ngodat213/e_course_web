﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class User
    {
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string username {get; set; }
    }

    public class UserResponse
    {
        public string message { get; set; }
        public string token { get; set; }
    }
}