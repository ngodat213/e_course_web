using e_course_web.DataQuery;
using e_course_web.Manager;
using e_course_web.Models;
using e_course_web.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace e_course_web.Controllers
{
    public class HomeController : Controller
    {
        // Call repository
        private readonly Repository<CoursesResponse> _courseRepository = new Repository<CoursesResponse>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            CoursesResponse query = await _courseRepository.GetAsync(ManagerAddress.domain, ManagerAddress.course);
            return View(query.courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}