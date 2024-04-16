using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
using e_course_web.Service.Helpers;
using e_course_web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Course")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public CourseController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [Route("")]
        public IActionResult Index()
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
        [Route("detail")]
        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            // Check user is register
            bool isRegister = false; ;
            if(_unitOfWork.UserOrder.GetFirstOrDefault(x => x.CourseId == id && x.UserId == _userManager.GetUserId(User)) != null)
            {
                isRegister = true;
            }

            Course course = _unitOfWork.Course.GetFirstOrDefault(i => i.Id == id, includeProperties: "Lessons,Category");

            // Get name user
            var teacherGetName = _unitOfWork.User.GetFirstOrDefault(p => p.Id == course.TeacherId).FullName;

            for(int i = 0; i < course.Lessons.Count; i++)
            {
                course.Lessons[i] = _unitOfWork.CourseLesson.GetFirstOrDefault(item => item.Id == course.Lessons[i].Id, includeProperties: "Videos");
            }
            if (course != null)
            {
                CourseDetailVM detailVM = new CourseDetailVM()
                {
                    IsRegister = isRegister,
                    Course = course,
                    TeacherName = teacherGetName,
                };
                return View(detailVM);
            }
            return NotFound();
        }
        [Route("detail")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Detail(int? id)
        {
            if(id!= null)
            {
                var course = await _unitOfWork.Course.GetById(id);
                UserOrder userOrder = new UserOrder()
                {
                    CourseId = id ?? 0,
                    UserId = _userManager.GetUserId(User),
                    TotalPrice = course.Price,
                    OrderDate = DateTime.Now,
                    Payment = SD.Payment_Momo,
                    PaymentStatus = "Success"
                };
                _unitOfWork.UserOrder.Add(userOrder);
                return RedirectToAction("Detail", new { id });
            }
            return RedirectToAction("Detail", new { id });
        }
    }
}
