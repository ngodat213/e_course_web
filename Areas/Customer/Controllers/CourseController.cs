using e_course_web.DataAccess.Repositorys;
using e_course_web.Manager;
using e_course_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Course")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("")]
        public IActionResult Index()
        {
            IEnumerable<Course> courses = _unitOfWork.Course.GetAll(includeProperties: "Lessons,Category");
            if (courses != null)
            {
                return View(courses.Take(10));
            }
            return View();
        }
        [Route("detail")]
        public async Task<IActionResult> Detail(int id)
        {
            Course course = _unitOfWork.Course.GetFirstOrDefault(i => i.Id == id, includeProperties: "Lessons,Feedbacks,Category");
            for(int i = 0; i < course.Lessons.Count; i++)
            {
                course.Lessons[i] = _unitOfWork.CourseLesson.GetFirstOrDefault(item => item.Id == course.Lessons[i].Id, includeProperties: "Videos");
            }
            if (course != null)
            {
                return View(course);
            }
            return NotFound();
        }
        [Route("order")]
        public IActionResult CourseOrdel(int id)
        {
            return View();
        }
    }
}
