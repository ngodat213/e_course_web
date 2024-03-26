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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            return View();
        }

        public async Task<IActionResult> SignUp()
        {
            return View();
        }
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        public async Task<IActionResult> Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}