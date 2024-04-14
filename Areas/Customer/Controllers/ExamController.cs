using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
using e_course_web.Service.Helpers;
using e_course_web.Service.Managers;
using e_course_web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Exam")]
    public class ExamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public ExamController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [Route("")]
        public IActionResult Index()
        {
            var exams = _unitOfWork.Exam.GetAll(includeProperties: "Lessons");
            if (exams != null)
            {
                return View(exams.Take(10));
            }
            return View(exams);
        }

        [Route("Detail")]
        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            Exam exam = _unitOfWork.Exam.GetFirstOrDefault(i => i.Id == id, includeProperties: "Lessons,Category");
            for (int i = 0; i < exam.Lessons.Count; i++)
            {
                exam.Lessons[i] = _unitOfWork.ExamLesson.GetFirstOrDefault(item => item.Id == exam.Lessons[i].Id, includeProperties: "ExamQuestion");
            }
            if (exam != null)
            {
                return View(exam);
            }
            return NotFound();
        }

        [Route("Test")]
        [Authorize]
        public IActionResult Test(int id)
        {
            ExamLesson examLesson = _unitOfWork.ExamLesson.GetFirstOrDefault(p => p.Id == id, includeProperties: "ExamQuestion");
            TestLessonVM lessonVM = new TestLessonVM
            {
                ExamId = id,
                Id = examLesson.Id,
                Hour = examLesson.Hour,
                Minute = examLesson.Minute,
                Second = examLesson.Second,
                Title = examLesson.Title,
                Lesson = examLesson.Lesson,
                Point = examLesson.Point,
            };
            var examQuestions = new List<TestQuestionVM>();
            foreach (var item in examLesson.ExamQuestion)
            {
                examQuestions.Add(new TestQuestionVM
                {
                    Id = item.Id,
                    Question = item.Question,
                    Answer = item.Answer,
                    ImageUrl = item.ImageUrl,
                    PublicId = item.PublicId,
                    Option = ConvertJsonHelper.FromJson<List<string>>(item.Option)
                });
            }
            lessonVM.ExamQuestions = examQuestions;
            SessionHelper.SetObjectAsJson(HttpContext.Session, ManagerKeyStorage.TEST_ID, lessonVM);
            return View(lessonVM);
        }

        [Route("Result")]
        [Authorize]
        public IActionResult ExamResult()
        {
            ExamResultVM examResultVM = SessionHelper.GetObjectFromJson<ExamResultVM>(HttpContext.Session, ManagerKeyStorage.RESULT_VM);
            return View(examResultVM);
        }

        #region API
        [Route("SubmitAnswers")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitAnswers([FromBody] List<ExamAnswer> userChooses)
        {
            TestLessonVM testLessonVM = SessionHelper.GetObjectFromJson<TestLessonVM>(HttpContext.Session, ManagerKeyStorage.TEST_ID);

            // Set data checks
            List<bool> checks = new List<bool>(); 
            for(int i = 0; i < testLessonVM.ExamQuestions.Count; i++) {
                checks.Add(userChooses[i].Answer == testLessonVM.ExamQuestions[i].Answer ? true : false);
            }
            // Get score
            double Score = checks.Count(i => i == true) * (testLessonVM!.Point / testLessonVM!.ExamQuestions.Count);
            ExamHistory examHistory = new ExamHistory()
            {
                UserId = _userManager.GetUserId(User),
                ExamId = testLessonVM.ExamId,
                SubmitDate = DateTime.Now,
                Point = Score,
            };
            /*_unitOfWork.ExamHistory.Add(examHistory);*/

            ExamResultVM examResultVM = new ExamResultVM()
            {
                Score = Score,
                testLessonVM = testLessonVM,
                UserChoice = userChooses,
            };
            SessionHelper.SetObjectAsJson(HttpContext.Session, ManagerKeyStorage.RESULT_VM, examResultVM);
            return Json(new { success = true, message = "Answers submitted successfully" });
        }
        #endregion
    }
}
