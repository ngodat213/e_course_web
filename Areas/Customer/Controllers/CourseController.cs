using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
using e_course_web.Service.Helpers;
using e_course_web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        // Search index default id: -1 = all
        [Route("")]
        public IActionResult Index()
        {
            IEnumerable<Course> courses;
            List<CourseVM> courseVM = new List<CourseVM>();
            ViewBag.Categories = _unitOfWork.Categories.GetAll();

            courses = _unitOfWork.Course.GetAll();
            if (courses != null)
            {
                foreach (var course in courses)
                {
                    var user = _unitOfWork.User.GetFirstOrDefault(p => p.Id == course.TeacherId);
                    courseVM.Add(new CourseVM() { Id = course.Id, ImageUrl = course.ImageUrl, PhotoUrl = user.PhotoUrl, Price = course.Price, TeacherName = user.FullName, Title = course.Title, CategoryId = course.CategoryId });
                }
                return View(courseVM);
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

            // Get name teacher user
            var teacherGet = _unitOfWork.User.GetFirstOrDefault(p => p.Id == course.TeacherId);

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
                    CourseFeedbacks = _unitOfWork.CourseFeedback.GetAll(f => f.CourseId == course.Id, includeProperties: "User"),
                    Instructor = new InstructorVM()
                    {
                        PhotoUrl = teacherGet.PhotoUrl,
                        Introduce = teacherGet.Introduce,
                        FullName = teacherGet.FullName,
                        Expert = teacherGet.Expert,
                        CourseCount = _unitOfWork.Course.GetAll(c => c.TeacherId == teacherGet.Id).Count(),
                        ReviewCount = _unitOfWork.CourseFeedback.GetAll(f => f.CourseId == course.Id).Count(),
                        Rating = _unitOfWork.CourseFeedback.GetAll(f => f.CourseId == course.Id).Count() != 0 ? _unitOfWork.CourseFeedback.GetAll(f => f.CourseId == course.Id).Average(p => p.Rating) : 5,
                    },
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
                Course course = await _unitOfWork.Course.GetById(id);
                UserOrder userOrder = new UserOrder()
                {
                    CourseId = id ?? 0,
                    UserId = _userManager.GetUserId(User),
                    TotalPrice = course.Price,
                    OrderDate = DateTime.Now,
                    Payment = SD.Payment_Momo,
                    PaymentStatus = "Success"
                };
                course.Register++;
                _unitOfWork.Course.Update(course);
                _unitOfWork.UserOrder.Add(userOrder);
                return RedirectToAction("Detail", new { id });
            }
            return RedirectToAction("Detail", new { id });
        }
        [Route("AddReview")]
        [HttpPost]
        public async Task<IActionResult> AddReview(int? id, ReviewVM model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Feedback.Add(new CourseFeedback()
                {
                    UserId = _userManager.GetUserId(User),
                    CourseId = (int)id,
                    Title = model.ReviewText,
                    Rating = model.Rating,
                });
            }
            return RedirectToAction("Detail", new { id });
        }
    }
}
