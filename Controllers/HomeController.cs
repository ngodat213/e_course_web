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
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            CourseResponse courseRequest = await _unitOfWork.CourseRespo.GetAsync(ManagerAddress.domain, ManagerAddress.course);
            if(courseRequest != null && courseRequest.courses != null)
            {
                IEnumerable<Course> limitedCourses = courseRequest.courses.Take(4);
                return View(limitedCourses);
            }
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        /*
         * LOGIN
         */
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if(ModelState.IsValid)
            {
                UserResponse request = await _unitOfWork.UserRespo.PostAsync(user, ManagerAddress.domain, ManagerAddress.login);
                if(request != null)
                {
                    // Decode token, save token to cookie

                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        /*
         * SIGN UP
         */
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            if(ModelState.IsValid && user.username != "") {
                await _unitOfWork.UserRespo.PostAsync(user, ManagerAddress.domain, ManagerAddress.signup);
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        /*
         * FORGOT PASSWORD
         */
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(String email)
        {
            return View();
        }
        public async Task<IActionResult> Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(Contact value)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.ContactRespo.PostAsync(value, ManagerAddress.domain, ManagerAddress.contact);
                return View();
            }
            return View();
        }
    }
}