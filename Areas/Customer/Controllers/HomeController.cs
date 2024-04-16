using e_course_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using e_course_web.DataAccess.Repositorys;
using e_course_web.ViewModels;

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
            IEnumerable<Course> courses = _unitOfWork.Course.GetAll();
            List<CourseVM> courseVM = new List<CourseVM>();

            if (courses != null)
            {
                foreach (var course in courses)
                {
                    var user = _unitOfWork.User.GetFirstOrDefault(p => p.Id == course.TeacherId);
                    courseVM.Add(new CourseVM() { Id = course.Id, ImageUrl = course.ImageUrl, PhotoUrl = user.PhotoUrl, Price = course.Price, TeacherName = user.FullName, Title = course.Title });
                }
                return View(courseVM.Take(10));
            }
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
                _unitOfWork.Contact.Add(value);
                return View();
            }
            return View();
        }
    }
}
