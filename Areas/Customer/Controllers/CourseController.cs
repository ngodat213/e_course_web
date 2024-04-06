using e_course_web.DataAccess.Repositorys;
using e_course_web.Manager;
using e_course_web.Models;
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

        public IActionResult Index()
        {
            IEnumerable<Course> courses = _unitOfWork.Course.GetAll();
            if (courses != null)
            {
                return View(courses.Take(4));
            }
            return View();
        }

        public async Task<IActionResult> CourseDetail(int id)
        {
            Course course = await _unitOfWork.Course.GetById(id);
            if (course != null)
            {
                return View(course);
            }
            return NotFound();
        }
    }
}
