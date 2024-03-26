using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
