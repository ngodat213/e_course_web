using System.ComponentModel.DataAnnotations;

namespace e_course_web.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email {  get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required]
        public bool Remember { get; set; }
    }
}
