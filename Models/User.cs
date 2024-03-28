using System.ComponentModel.DataAnnotations;

namespace e_course_web.Models
{
    public class User
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public string? username { get; set; }
        public string? photoUrl { get; set; }
        public string? token { get; set; }
        public List<string>? blogs { get; set; }
        public List<string>? qAs { get; set; }
        public List<string>? courses { get; set; }
        public List<string>? favoriteCourses { get; set; }
        public List<string>? favoriteQuizs { get; set; }
        public List<string>? favoriteTeachers { get; set; }
        public List<string>? finishedQuizs { get; set; }
        public List<string>? favoriteBlog { get; set; }
        public List<string>? favoritesQAs { get; set; }
    }

    public class UserResponse
    {
        public string message { get; set; }
        public string token { get; set; }
    }
}