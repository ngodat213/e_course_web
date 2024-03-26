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
        public string? username {get; set; }
        public string? photoUrl { get; set; }
        public string? token { get; set; }
        public List<String>? blogs { get; set; }
        public List<String>? qAs {  get; set; }
        public List<String>? courses { get; set; }
        public List<String>? favoriteCourses { get; set; }
        public List<String>? favoriteQuizs { get; set; }
        public List<String>? favoriteTeachers { get; set; }
        public List<String>? finishedQuizs { get; set; }
        public List<String>? favoriteBlog { get; set; }
        public List<String>? favoritesQAs {  get; set; }
    }

    public class UserResponse
    {
        public string message { get; set; }
        public string token { get; set; }
    }
}