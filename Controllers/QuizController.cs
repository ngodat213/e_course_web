using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
