using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
