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

        public async Task<IActionResult> Create()
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
            Exam exam = _unitOfWork.Exam.GetFirstOrDefault(c => c.Id == id, includeProperties: "Lessons");
            if (exam != null)
            {
                ExamLessonVM courseLessonVM = new ExamLessonVM()
                {
                    Exam = exam,
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
            // Edit exam detail
            if(value.Exam != null && value.Exam.Title != null && value.Exam.CategoryId != null && value.Exam.Description != null)
            {
                Exam exam = await _unitOfWork.Exam.GetById(id);
                ImageUploadResult cloudImage = null;
                if(value.Image != null)
                {
                    cloudImage = await _cloudinaryService.AddPhotoAsync(value.Image, ManagerAddress.PublicId_ExamImage);
                }
                exam.Title = value.Exam.Title;
                exam.CategoryId = value.Exam.CategoryId;
                exam.Description = value.Exam.Description;
                exam.ImageUrl = value.Image != null ? cloudImage.Url.ToString() : exam.ImageUrl;
                exam.PublicId = value.Image != null ? cloudImage.PublicId: exam.PublicId;

                _unitOfWork.Exam.Update(exam);
            }
            // Add exam lesson
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
            List<Option> options = new List<Option>
            {
                new Option { Id = 0, Opt = "A" },
                new Option { Id = 1, Opt = "B" },
                new Option { Id = 2, Opt = "C" },
                new Option { Id = 3, Opt = "D" }
            };
            ViewBag.Options = options.Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Opt }).ToList();
            ExamLesson examLesson = _unitOfWork.ExamLesson.GetFirstOrDefault(i => i.Id == id, includeProperties: "ExamQuestion");
            if (examLesson != null)
            {
                ExamQuestionVM examQuestionVM = new ExamQuestionVM()
                {
                    ExamLesson = examLesson,
                };
                return View(examQuestionVM);
            }
            return RedirectToAction("Lesson", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Lesson(int? id, ExamQuestionVM value)
        {
            // Edit lesson
            if (value.ExamLesson != null && value.ExamLesson.Title != null && value.ExamLesson.Lesson != null && value.ExamLesson.Hour != null &&
                value.ExamLesson.Minute != null && value.ExamLesson.Second != null && value.ExamLesson.Point != null)
            {
                ExamLesson examLesson = await _unitOfWork.ExamLesson.GetById(id);
                if (examLesson != null)
                {
                    examLesson.Title = value.ExamLesson.Title;
                    examLesson.Lesson = value.ExamLesson.Lesson;
                    examLesson.Hour = value.ExamLesson.Hour;
                    examLesson.Minute = value.ExamLesson.Minute;
                    examLesson.Second = value.ExamLesson.Second;
                    examLesson.Point = value.ExamLesson.Point;

                    _unitOfWork.ExamLesson.Update(examLesson);
                }
            }

            // Add question
            if (value.Question != null && value.Answer != null && value.Option1 != null && value.Option2 != null && value.Option3 != null && value.Option4 != null)
            {
                ImageUploadResult cloud = new ImageUploadResult();
                if (value.Image != null)
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
            return RedirectToAction("Detail");
        }

        public async Task<IActionResult> Question(int? id)
        {
            ExamQuestion examQuestion = await _unitOfWork.ExamQuestion.GetById(id);
            if(examQuestion != null) 
            {
                return View(new ExamQuestionDetailVM() { 
                    Question = examQuestion.Question, 
                    Answer = examQuestion.Answer, 
                    Option = ConvertJsonHelper.FromJson<List<string>>(examQuestion.Option),
                    ImageUrl = examQuestion.ImageUrl,
                });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Question(int? id, ExamQuestionDetailVM value)
        {
            ExamQuestion examQuestion= await _unitOfWork.ExamQuestion.GetById(id);

            if (value.Question != null && value.Answer != null && value.Option1 != null && value.Option2 != null &&
                value.Option3 != null && value.Option4 != null)
            {
                examQuestion.Question = value.Question;
                examQuestion.Answer = value.Answer;
                examQuestion.Option = ConvertJsonHelper.ToJson(new List<string>(){ value.Option1,
                            value.Option2,
                            value.Option3,
                            value.Option4,
                    });

                if (value.Image != null)
                {
                    var cloud = await _cloudinaryService.AddPhotoAsync(value.Image, ManagerAddress.PublicId_ExamQuestions);
                    if (cloud.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        examQuestion.ImageUrl = cloud.Url.ToString();
                        examQuestion.PublicId = cloud.PublicId;
                    }
                }
                _unitOfWork.ExamQuestion.Update(examQuestion);
            }
            return RedirectToAction("Question", new { id });
        }

    }
}
