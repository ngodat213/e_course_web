using e_course_web.Manager;
using e_course_web.Models;
using e_course_web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            CourseResponse courseRequest = await _unitOfWork.CourseRespo.GetAsync(ManagerAddress.domain, ManagerAddress.course);
            if (courseRequest != null && courseRequest.courses != null)
            {
                IEnumerable<Course> limitedCourses = courseRequest.courses.Take(4);
                return View(limitedCourses);
            }
            return View();
        }

        public async Task<IActionResult> CourseDetail(string id)
        {
            List<CourseLesson> courseLessons = new List<CourseLesson>();
            Course course = await _unitOfWork.CourseRespo.GetAsync(id, ManagerAddress.domain, ManagerAddress.course);
            if (course != null && course.Lessons != null)
            {
                foreach (var lesson in course.Lessons)
                {
                    courseLessons.Add(await _unitOfWork.CourseLessonRespo.GetAsync(lesson, ManagerAddress.domain, ManagerAddress.courseLesson));
                }
                ViewBag.CourseLessons = courseLessons;
                return View(course);
            }
            return NotFound();
        }
    }
}
