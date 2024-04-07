using Microsoft.AspNetCore.Mvc;
using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using e_course_web.ViewModels;
using e_course_web.Service.Interfaces;
using e_course_web.Service.Helpers;
using CloudinaryDotNet.Actions;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;


        public ExamController(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        // Address https://localhost:7189/admin/question
        // Description: Show all course
        // Data: IEnumerable<Quiz>
        public async Task<IActionResult> Index()
        {
            IEnumerable<Exam> quizs = _unitOfWork.Exam.GetAll(includeProperties: "Lessons");
            return View(quizs);
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
        public async Task<IActionResult> Create(QuestionVM value)
        {
            if (ModelState.IsValid)
            {
                var cloud = await _cloudinaryService.AddPhotoAsync(value.Image, isCourse: false);
                Exam quiz = new Exam()
                {
                    Title = value.Title,
                    CategoryId = value.CategoryId,
                    Description = value.Description,
                    ImageUrl = cloud.Url.ToString(),
                    UpdateAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                };
                _unitOfWork.Exam.Add(quiz);
            }
            return View();
        }

        public async Task<IActionResult> Detail(int? id)
        {
            Exam question = await _unitOfWork.Exam.GetById(id);
            if (question != null)
            {
                ViewBag.Question = question;
                IEnumerable<ExamLesson> questionLessons = _unitOfWork.ExamLesson.GetAll(i => i.Id == question.Id);
                QuestionLessonVM courseLessonVM = new QuestionLessonVM()
                {
                    QuestionLessons = questionLessons,
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
        // Address https://localhost:7189/admin/question/detail/{id?}
        // Description :Add lesson for course
        // Data return: idLesson, CourseLessonVM
        [HttpPost]
        public async Task<IActionResult> Detail(int? id, QuestionLessonVM value)
        {
            if (value.Title != null && value.Lesson != null && value.Point != null && value.Hour != null && value.Second != null && value.Minute != null)
            {
                ExamLesson lesson = new ExamLesson()
                {
                    Title = value.Title,
                    Lesson = value.Lesson,
                    Point = value.Point,
                    Hour = value.Hour,  
                    Minute = value.Minute,
                    Second = value.Second
                };
                Exam question = await _unitOfWork.Exam.GetById(id);
                var courseLessonList = new List<ExamLesson>() {
                    lesson
                };
                question.Lessons = courseLessonList;
                _unitOfWork.Exam.Update(question);
            }
            // Reload page
            return RedirectToAction("Detail", new { id });
        }

        // Address https://localhost:7189/admin/question/examquestion/{id?}
        // Show detail video, and edit video
        public async Task<IActionResult> Lesson(int? id)
        {
            ExamLesson examLesson = await _unitOfWork.ExamLesson.GetById(id);
            if (examLesson != null)
            {
                ViewBag.CourseLesson = examLesson;
                IEnumerable<ExamQuestion> questionTests = _unitOfWork.ExamQuestion.GetAll(i => i.Id == examLesson.Id);
                ExamQuestionVM examQuestionVM = new ExamQuestionVM()
                {
                    QuestionTest = questionTests,
                };
                return View(examQuestionVM);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Lesson(int? id, ExamQuestionVM value)
        {
            if (value.Question != null && value.Answer != null && value.Option1 != null && value.Option2 != null && value.Option3 != null && value.Option4 != null)
            {
                try
                {
                    ImageUploadResult cloud = new ImageUploadResult();
                    if(value.Image != null)
                    {
                        cloud = await _cloudinaryService.AddPhotoAsync(value.Image);
                    }
                    // Convert List<string> option to json
                    var jsonConvert = ConvertJsonHelper.ToJson(new List<string>(){ value.Option1,
                        value.Option2,
                        value.Option3,
                        value.Option4,
                    });
                    ExamQuestion examQuestion = new ExamQuestion()
                    {
                        Question = value.Question,
                        Answer = value.Answer,
                        Option = jsonConvert,
                        ImageUrl = value.Image == null ? cloud.Url.ToString() : null,
                    };
                    ExamLesson examLesson = await _unitOfWork.ExamLesson.GetById(id);
                    var videoList = new List<ExamQuestion>(){
                        examQuestion,
                     };
                    examLesson.ExamQuestion = videoList;
                    _unitOfWork.ExamLesson.Update(examLesson);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }
            }
            // Reload page
            return RedirectToAction("Lesson", new { id });
        }
    }
}
