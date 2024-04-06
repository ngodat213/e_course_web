using e_course_web.Manager;
using e_course_web.Models;
using e_course_web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using e_course_web.ModelViews;
using e_course_web.DataAccess.Repositorys;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public  IActionResult Index()
        {
            IEnumerable<Course> courses =  _unitOfWork.Course.GetAll(includeProperties: "Lessons,Feedbacks,Category");
            if (courses != null)
            {
                return View(courses.Take(4));
            }
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        
         /** LOGIN*/

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM value)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.PasswordSignInAsync(value.Email, value.Password, value.Remember, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Invalid login attemp");
                return View(value);
            }

            return View(value);
        }
        
         /** SIGN UP*/

        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            /*if (ModelState.IsValid && user.username != "")
            {
                await _unitOfWork.UserRespo.PostAsync(user, ManagerAddress.domain, ManagerAddress.signup);
                return RedirectToAction(nameof(Login));
            }*/
            return View();
        }
        
         /** FORGOT PASSWORD*/

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
                /*await _unitOfWork.ContactRespo.PostAsync(value, ManagerAddress.domain, ManagerAddress.contact);*/
                return View();
            }
            return View();
        }
    }
}
