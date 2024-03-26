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
        private readonly IRepository<User, UserResponse> _userRepository;

        public HomeController(ILogger<HomeController> logger, IRepository<User, UserResponse> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
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
            if(ModelState.IsValid)
            {
                UserResponse request = await _userRepository.PostAsync(user, ManagerAddress.domain, ManagerAddress.login);
                if(request != null)
                {
                    // Decode token, save token to cookie

                    return RedirectToAction(nameof(Index));
                }
            }
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