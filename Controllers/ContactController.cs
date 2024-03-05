using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
