using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Controllers
{
    public class CourseLessonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
