using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
