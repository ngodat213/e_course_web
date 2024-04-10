using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
using e_course_web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using e_course_web.Service.Interfaces;
using e_course_web.Service.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        // Controller: IUnitOfWork
        public CourseController(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }
        // Address https://localhost:7189/admin/course
        // Description: Show all course
        // Data: IEnumerable<Course>
        public IActionResult Index()
        {
            IEnumerable<Course> courses = _unitOfWork.Course.GetAll();
            return View(courses);
        }
        // Address https://localhost:7189/admin/course/create
        // Description: Create course
        // Data input: idCourse
        // Data: ViewBag.Categories
        public async Task<IActionResult> Create(int? id)
        {
           
            ViewBag.Categories = _unitOfWork.Categories.GetAll().Select(i => new SelectListItem
            {
                Text = i.Category,
                Value = i.Id.ToString()
            });
            return View();
        }
        // Address https://localhost:7189/admin/course/create
        // Description: Add course
        // Data input: CourseVM
        [HttpPost]
        public async Task<IActionResult> Create(CourseVM value)
        {
            if(ModelState.IsValid)
            {
                var cloud = await _cloudinaryService.AddPhotoAsync(value.CourseImage);
                Course course = new Course()
                {
                    Title = value.Title,
                    Price = value.Price,
                    CategoryId = value.CategoryId,
                    Description = value.Description,
                    Rating = 0,
                    Register = 0,
                    TeacherId = value.TeacherId,
                    ImageUrl = cloud.Url.ToString(),
                    Time = value.Time,
                    Language = value.Language,
                    UpdateAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                };
                _unitOfWork.Course.Add(course);
            }
            return RedirectToAction("Create");
        }
        // Address https://localhost:7189/admin/course/detail/{id?}
        // Description: detail course
        // Data: ViewBag.Course, IEnumerable<CourseLesson>
        public async Task<IActionResult> Detail(int? id)
        {
            Course course = await _unitOfWork.Course.GetById(id);
            if (course != null)
            {
                ViewBag.Course = course;
                // Danh sach lesson
                IEnumerable<CourseLesson> courseLessons = _unitOfWork.CourseLesson.GetAll().OrderBy(i => i.Id == course.Id);
                CourseLessonVM courseLessonVM = new CourseLessonVM()
                {
                    CourseLesson = courseLessons,
                };
                ViewBag.Categories = _unitOfWork.Categories.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Category,
                    Value = i.Id.ToString()
                });
                return View(courseLessonVM);
            }
            return RedirectToAction(nameof(Index));
        }
        // Address https://localhost:7189/admin/course/detail/{id?}
        // Description :Add lesson for course
        // Data return: idLesson, CourseLessonVM
        [HttpPost]
        public async Task<IActionResult> Detail(int? id, CourseLessonVM value)
        {
            if (value.Title != null && value.Lesson != null)
            {
                CourseLesson courseLesson = new CourseLesson()
                {
                    Title = value.Title,
                    Lesson = value.Lesson,
                };
                Course course = await _unitOfWork.Course.GetById(id);
                var lessonList = new List<CourseLesson>() {
                    courseLesson
                };
                course.Lessons = lessonList;
                _unitOfWork.Course.Update(course);
            }
            // Reload page
            return RedirectToAction("Detail", new { id });
        }
        // Address https://localhost:7189/admin/course/lesson/{id?}
        // Show detail lesson, add video course
        // Data: ViewBag.CourseLesson
        public async Task<IActionResult> Lesson(int? id)
        {
            CourseLesson courseLesson = await _unitOfWork.CourseLesson.GetById(id);
            if(courseLesson != null)
            {
                ViewBag.CourseLesson = courseLesson;
                IEnumerable<CourseVideo> courseVideos = _unitOfWork.CourseVideo.GetAll().OrderBy(i => i.Id == courseLesson.Id);
                CourseVideoVM courseVideoVM = new CourseVideoVM()
                {
                    CourseVideo = courseVideos,
                };
                return View(courseVideoVM);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Lesson(int? id, CourseVideoVM value)
        {
            if (value.Title != "" && value.Hour != null && value.Part != null && value.Minute != null && value.Video != null)
            {
                try {
                    var cloud = await _cloudinaryService.AddVideoAsync(value.Video);
                    CourseVideo courseVideo = new CourseVideo()
                    {
                        Part = value.Part,
                        Title = value.Title,
                        Hour = value.Hour,
                        Minute = value.Minute,
                        VideoUrl = cloud.Url.ToString(),
                    };
                    CourseLesson courseLesson = await _unitOfWork.CourseLesson.GetById(id);
                    var videoList = new List<CourseVideo>(){
                        courseVideo,
                     };
                    courseLesson.Videos = videoList;
                    _unitOfWork.CourseLesson.Update(courseLesson);

                }
                catch(Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }
            }
            // Reload page
            return RedirectToAction("Lesson", new { id });
            
        }
        // Address https://localhost:7189/admin/course/video/{id?}
        // Show detail video, and edit video
        public async Task<IActionResult> Video(int? id)
        {
            CourseVideo courseVideo = await _unitOfWork.CourseVideo.GetById(id);
            if (courseVideo != null)
            {
                return View(courseVideo);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Video(int? id, CourseVideo video)
        {
            CourseVideo courseVideo = await _unitOfWork.CourseVideo.GetById(id);
            if(courseVideo != null)
            {
                _unitOfWork.CourseVideo.Update(video);

            }
            return View();
        }
    }
}
