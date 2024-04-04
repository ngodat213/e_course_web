using e_course_web.Models;
using e_course_web.Repositorys;
using e_course_web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            return View(courses);
        }

        public async Task<IActionResult> Create(int? id)
        {
           
            ViewBag.Categories = _unitOfWork.Categories.GetAll().Select(i => new SelectListItem
            {
                Text = i.Category,
                Value = i.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        public IActionResult Create(CourseVM value)
        {
            if(ModelState.IsValid)
            {
                Course course = new Course()
                {
                    Title = value.Title,
                    Price = value.Price,
                    CategoryId = value.CategoryId,
                    Description = value.Description,
                    Rating = 0,
                    Register = 0,
                    TeacherId = value.TeacherId,
                    CourseImage = value.CourseImage,
                    Time = value.Time,
                    Language = value.Language,
                    UpdateAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                };
                _unitOfWork.Course.Add(course);
            }
            return View(value);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id != null)
            {
                Course course = await _unitOfWork.Course.GetById(id);
                return View(course);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int? id, CourseLessonVM value)
        {
            if (ModelState.IsValid)
            {
                CourseLesson courseLesson = new CourseLesson()
                {
                    Title = value.Title,
                    Lesson = value.Lesson,
                };
                Course course = await _unitOfWork.Course.GetById(id);
                course.Lessons.Add(courseLesson);
                _unitOfWork.Course.Update(course);
            }
            return View();
        }

        public IActionResult Feedback()
        {
            return View();
        }
    }
}
