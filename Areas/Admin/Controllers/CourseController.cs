using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCourse()
        {
            return View();
        }

        public IActionResult EditCourse()
        {
            return View();
        }
        public IActionResult Feedback()
        {
            return View();
        }
    }
}
