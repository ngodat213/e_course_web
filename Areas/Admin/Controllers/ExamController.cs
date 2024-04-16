using Microsoft.AspNetCore.Mvc;
using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using e_course_web.ViewModels;
using e_course_web.Service.Interfaces;
using e_course_web.Service.Helpers;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using e_course_web.Service.Manager;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Teacher)]
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

        [HttpPost]
        public async Task<IActionResult> IndexDelete(int id)
        {
            Exam exam = _unitOfWork.Exam.GetFirstOrDefault(i => i.Id == id, includeProperties: "Lessons");
            if (exam != null)
            {
                foreach(var lesson in exam.Lessons)
                {
                    var value = _unitOfWork.ExamLesson.GetFirstOrDefault(i => i.Id == lesson.Id, includeProperties: "ExamQuestion");
                    foreach(var question in value.ExamQuestion)
                    {
                        _unitOfWork.ExamQuestion.Delete(question);
                    }
                    _unitOfWork.ExamLesson.Delete(lesson);
                }
                _unitOfWork.Exam.Delete(exam);
            }
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Create(ExamVM value)
        {
            if (ModelState.IsValid)
            {
                var cloud = await _cloudinaryService.AddPhotoAsync(value.Image, ManagerAddress.PublicId_ExamQuestions);
                if(cloud.StatusCode  == System.Net.HttpStatusCode.OK)
                {
                    Exam quiz = new Exam()
                    {
                        Title = value.Title,
                        CategoryId = value.CategoryId,
                        Description = value.Description,
                        ImageUrl = cloud.Url.ToString(),
                        PublicId = cloud.PublicId,
                        UpdateAt = DateTime.Now,
                        CreatedAt = DateTime.Now,
                    };
                    _unitOfWork.Exam.Add(quiz);
                }
            }
            return View();
        }

        public async Task<IActionResult> Detail(int? id)
        {
            Exam question = await _unitOfWork.Exam.GetById(id);
            if (question != null)
            {
                ViewBag.Question = question;
                IEnumerable<ExamLesson> questionLessons = _unitOfWork.ExamLesson.GetAll().OrderBy(i => i.Id == question.Id);
                ExamLessonVM courseLessonVM = new ExamLessonVM()
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
        public async Task<IActionResult> Detail(int? id, ExamLessonVM value)
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

        [HttpPost]
        public IActionResult DetailDelete(int id)
        {
            ExamLesson examLesson =  _unitOfWork.ExamLesson.GetFirstOrDefault(i => i.Id == id, includeProperties: "ExamQuestion");
            if (examLesson != null)
            {
                foreach (var question in examLesson.ExamQuestion)
                {
                    if(question.ImageUrl != null)
                    {
                        _cloudinaryService.RemoveAsync(question.PublicId);
                    }
                    _unitOfWork.ExamQuestion.Delete(question);
                }
                _unitOfWork.ExamLesson.Delete(examLesson);
            }
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
                IEnumerable<ExamQuestion> questionTests = _unitOfWork.ExamQuestion.GetAll().OrderBy(i => i.Id == examLesson.Id);
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
                        cloud = await _cloudinaryService.AddPhotoAsync(value.Image, ManagerAddress.PublicId_ExamQuestions);
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
                        ImageUrl = value.Image != null ? cloud.Url.ToString() : null,
                        PublicId = value.Image != null ? cloud.PublicId : null,
                    };
                    if (cloud.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        
                        examQuestion.ImageUrl = value.Image != null ? cloud.Url.ToString() : null;
                        examQuestion.PublicId = value.Image != null ? cloud.PublicId : null;
                    }
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

        [HttpPost]
        public async Task<IActionResult> QuestionDelete(int? id)
        {
            var question = await _unitOfWork.ExamQuestion.GetById(id);
            if (question != null)
            {
                _unitOfWork.ExamQuestion.Delete(question);
            }
            return RedirectToAction("Lesson", new { id });
        }

        public async Task<IActionResult> Question(int? id)
        {
            ExamQuestion examQuestion = await _unitOfWork.ExamQuestion.GetById(id);
            if(examQuestion != null) {
                ExamQuestionDetailVM detail = new ExamQuestionDetailVM() {
                    Question = examQuestion.Question,
                    Answer = examQuestion.Answer,
                    ImageUrl = examQuestion.ImageUrl,
                    Option = ConvertJsonHelper.FromJson<List<string>>(examQuestion.Option)
                };
                return View(detail);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
