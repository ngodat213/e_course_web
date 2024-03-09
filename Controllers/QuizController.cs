using e_course_web.DataQuery;
using e_course_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Controllers
{
    public class QuizController : Controller
    {
        public async Task<IActionResult> Index()
        {
            CoursesResponse query = await QueryData.courseGetAll();
            return View(query.courses);
        }
    }
}
