using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace e_course_web.Models
{
    public class User : IdentityUser
    {
        /*[Required]
        public string username { get; set; }*/
        public string? photoUrl { get; set; }
        public ICollection<Blog>? blogs { get; set; }
        public ICollection<Blog>? qAs { get; set; }
        public ICollection<Course>? courses { get; set; }
        public ICollection<Course>? favoriteCourses { get; set; }
        public ICollection<Quiz>? favoriteQuizs { get; set; }
        public ICollection<User>? favoriteTeachers { get; set; }
        public ICollection<Quiz>? finishedQuizs { get; set; }
        public ICollection<Blog>? favoriteBlog { get; set; }
    }
}