using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
using e_course_web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using e_course_web.Service.Interfaces;
using e_course_web.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using e_course_web.Service.Manager;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Teacher)]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly UserManager<User> _userManager;
        // Controller: IUnitOfWork
        public CourseController(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
            _userManager = userManager;
        }
        // Address https://localhost:7189/admin/course
        // Description: Show all course
        // Data: IEnumerable<Course>
        public IActionResult Index()
        {
            IEnumerable<Course> courses;
            if (User.IsInRole(SD.Role_Teacher))
            {
                courses = _unitOfWork.Course.GetAll(p => p.TeacherId == _userManager.GetUserId(User));
                return View(courses);
            }
            courses = _unitOfWork.Course.GetAll();
            return View(courses);
        }
        // Address https://localhost:7189/admin/course/deleteindex/{id}
        // Description: Delete course
        // Data: IEnumerable<Course>
        [HttpPost]
        public async Task<IActionResult> IndexDelete(int id)
        {
            Course course = _unitOfWork.Course.GetFirstOrDefault(i => i.Id == id, includeProperties: "Lessons");
            if (course != null)
            {
                foreach (var lesson in course.Lessons)
                {
                    var value = _unitOfWork.CourseLesson.GetFirstOrDefault(i => i.Id == lesson.Id, includeProperties: "Videos");
                    foreach (var video in value.Videos)
                    {
                        _unitOfWork.CourseVideo.Delete(video);
                    }
                    _unitOfWork.CourseLesson.Delete(lesson);
                }
                _unitOfWork.Course.Delete(course);
            }
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Create(CourseInputVM value)
        {
            string TeacherID = null;
            if (User.IsInRole(SD.Role_Teacher))
            {
                TeacherID = _userManager.GetUserId(User);
            }
            // Add
            if (ModelState.IsValid)
            {
                var cloudImage = await _cloudinaryService.AddPhotoAsync(value.CourseImage, ManagerAddress.PublicId_CourseImages);
                var cloudVideo = await _cloudinaryService.AddVideoAsync(value.IntroductVideo, ManagerAddress.PublicId_CourseIntroduces);
                Course course = new Course()
                {
                    Title = value.Title,
                    Price = value.Price,
                    CategoryId = value.CategoryId,
                    Description = value.Description,
                    Rating = 0,
                    Register = 0,
                    TeacherId = TeacherID != null ? TeacherID : value.TeacherId,
                    ImageUrl = cloudImage.Url.ToString(),
                    PublicId = cloudImage.PublicId,
                    VideoUrl = cloudVideo.Url.ToString(),
                    PublicVideo = cloudVideo.PublicId,
                    Time = value.Time,
                    Language = value.Language,
                    UpdateAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                };
                _unitOfWork.Course.Add(course);
            }
            return RedirectToAction("Index");
        }
        // Address https://localhost:7189/admin/course/detail/{id?}
        // Description: detail course
        // Data: ViewBag.Course, IEnumerable<CourseLesson>
        public async Task<IActionResult> Detail(int? id)
        {
            Course course = _unitOfWork.Course.GetFirstOrDefault(c => c.Id == id, includeProperties: "Lessons");
            if (course != null)
            {
                // Danh sach lesson
                CourseLessonVM courseLessonVM = new CourseLessonVM()
                {
                    Course = course,
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
            // Edit course
            if (value.Course != null && value.Course.Title != null && value.Course.Price != null && value.Course.CategoryId != null && 
                value.Course.Description != null && value.Course.TeacherId != null && value.Course.Time != null && 
                value.Course.Language != null)
            {
                Course courseGet = await _unitOfWork.Course.GetById(id);
                ImageUploadResult cloudImage = null;
                VideoUploadResult cloudVideo = null;
                if (value.CourseImage != null)
                {
                    cloudImage = await _cloudinaryService.AddPhotoAsync(value.CourseImage, ManagerAddress.PublicId_CourseImages);
                }
                if (value.IntroductVideo != null)
                {
                    cloudVideo = await _cloudinaryService.AddVideoAsync(value.IntroductVideo, ManagerAddress.PublicId_CourseIntroduces);
                }
                courseGet.Title = value.Course.Title;
                courseGet.Price = value.Course.Price;
                courseGet.CategoryId = value.Course.CategoryId;
                courseGet.Description = value.Course.Description;
                courseGet.TeacherId = value.Course.TeacherId;
                courseGet.ImageUrl = cloudImage != null ? cloudImage.Url.ToString() : courseGet.ImageUrl;
                courseGet.PublicId = cloudImage != null ? cloudImage.PublicId : courseGet.PublicId;
                courseGet.VideoUrl = cloudVideo != null ? cloudVideo.Url.ToString() : courseGet.VideoUrl;
                courseGet.PublicVideo = cloudVideo != null ? cloudVideo.PublicId : courseGet.PublicVideo;
                courseGet.Time = value.Course.Time;
                courseGet.Language = value.Course.Language;
                courseGet.UpdateAt = DateTime.Now;

                _unitOfWork.Course.Update(courseGet);
            }
            // Add lesson
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

                return RedirectToAction("Detail", new { id });
            }
            // Reload page
            return RedirectToAction("Detail", new { id });
        }
        // Address https://localhost:7189/admin/course/detail/detaildelete{id?}
        // Description :Add lesson for course
        // Data return: idLesson, CourseLessonVM
        [HttpPost]
        public IActionResult DetailDelete(int id)
        {
            CourseLesson courseLesson = _unitOfWork.CourseLesson.GetFirstOrDefault(i => i.Id == id, includeProperties: "Videos");
            if (courseLesson != null)
            {
                foreach (var video in courseLesson.Videos)
                {
                    _cloudinaryService.RemoveAsync(video.PublicId);
                    _unitOfWork.CourseVideo.Delete(video);
                }
                _unitOfWork.CourseLesson.Delete(courseLesson);
            }
            return RedirectToAction("Detail", new { id });
        }
        // Address https://localhost:7189/admin/course/lesson/{id?}
        // Show detail lesson, add video course
        // Data: ViewBag.CourseLesson
        public async Task<IActionResult> Lesson(int? id)
        {
            CourseLesson courseLesson = _unitOfWork.CourseLesson.GetFirstOrDefault(i => i.Id == id, includeProperties: "Videos");
            if(courseLesson != null)
            {
                CourseVideoVM courseVideoVM = new CourseVideoVM()
                {
                    CourseLesson = courseLesson,
                };
                return View(courseVideoVM);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Lesson(int? id, CourseVideoVM value)
        {
            // Edit lesson
            if (value.CourseLesson != null && value.CourseLesson.Title != null && value.CourseLesson.Lesson != null && id != null)
            {
                CourseLesson lesson = await _unitOfWork.CourseLesson.GetById(id);
                if (lesson != null)
                {
                    lesson.Title = value.CourseLesson.Title;
                    lesson.Lesson = value.CourseLesson.Lesson;

                    _unitOfWork.CourseLesson.Update(lesson);
                }
            } 
            // Add question
            if (value.Title != "" && value.Hour != null && value.Part != null && value.Minute != null && value.Video != null)
            {
                var cloud = await _cloudinaryService.AddVideoAsync(value.Video, ManagerAddress.PublicId_CourseVideos);
                CourseVideo courseVideo = new CourseVideo()
                {
                    Part = value.Part,
                    Title = value.Title,
                    Hour = value.Hour,
                    Minute = value.Minute,
                    PublicId = cloud.PublicId,
                    VideoUrl = cloud.Url.ToString(),
                };
                CourseLesson courseLesson = await _unitOfWork.CourseLesson.GetById(id);
                var videoList = new List<CourseVideo>(){
                        courseVideo,
                     };
                courseLesson.Videos = videoList;
                _unitOfWork.CourseLesson.Update(courseLesson);
            }
            // Reload page
            return RedirectToAction("Lesson", new { id });
            
        }
        // Address https://localhost:7189/admin/course/lesson/lessondelete{id?}
        // Show detail lesson, add video course
        // Data: ViewBag.CourseLesson
        [HttpPost]
        public async Task<IActionResult> VideoDelete(int? id)
        {
            var video = await _unitOfWork.CourseVideo.GetById(id);
            if (video != null)
            {
                _unitOfWork.CourseVideo.Delete(video);
            }
            return RedirectToAction("Lesson", new { id });
        }
        // Address https://localhost:7189/admin/course/video/{id?}
        // Show detail video, and edit video
        public async Task<IActionResult> Video(int? id)
        {
            CourseVideo courseVideo = await _unitOfWork.CourseVideo.GetById(id);
            if (courseVideo != null)
            {
                return View(new VideoVM() { CourseVideo = courseVideo});
            }
            return RedirectToAction("Video", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Video(int? id, VideoVM value)
        {
            CourseVideo courseVideo = await _unitOfWork.CourseVideo.GetById(id);

            if (value.CourseVideo != null)
            {
                courseVideo.Hour = value.CourseVideo.Hour != null ? value.CourseVideo.Hour : courseVideo.Hour;
                courseVideo.Minute = value.CourseVideo.Minute != null ? value.CourseVideo.Minute : courseVideo.Minute;
                courseVideo.Title = value.CourseVideo.Title != null ? value.CourseVideo.Title : courseVideo.Title;
                courseVideo.Part = value.CourseVideo.Part != null ? value.CourseVideo.Part : courseVideo.Part;
                if (value.Video != null)
                {
                    var cloud = await _cloudinaryService.AddVideoAsync(value.Video, ManagerAddress.PublicId_CourseVideos);
                    if (cloud.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        courseVideo.VideoUrl = cloud.Url.ToString();
                        courseVideo.PublicId = cloud.PublicId;
                    }
                }
                _unitOfWork.CourseVideo.Update(courseVideo);
            }

            return RedirectToAction("Video", new { id });
        }
    }
}
