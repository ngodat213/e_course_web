using e_course_web.Manager;
using e_course_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuestionController : Controller
    {
        /*private readonly IUnitOfWork _unitOfWork;

        public QuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            QuizResponse quizResponse = await _unitOfWork.QuizRespo.GetAsync(ManagerAddress.domain, ManagerAddress.quiz);
            if (quizResponse != null)
            {
                return View(quizResponse.quizs);
            }
            return View();
        }

        public async Task<IActionResult> QuestionDetail(string id)
        {
            List<QuizLesson> quizLessons = new List<QuizLesson>();
            Quiz quiz = await _unitOfWork.QuizRespo.GetAsync(id, ManagerAddress.domain, ManagerAddress.quiz);
            if (quiz != null && quiz.Lessons != null)
            {
                foreach(var lesson in quiz.Lessons)
                {
                    quizLessons.Add(await _unitOfWork.QuizLessonRespo.GetAsync(lesson, ManagerAddress.domain, ManagerAddress.quizLesson));
                }
                ViewBag.QuizLessons = quizLessons;
                return View(quiz);
            }
            return View();
        }
        // Code !!!!
        public async Task<IActionResult> QuestionPlay(string id)
        {
            List<QuizLesson> quizLessons = new List<QuizLesson>();
            Quiz quiz = await _unitOfWork.QuizRespo.GetAsync(id, ManagerAddress.domain, ManagerAddress.quiz);
            if (quiz != null && quiz.Lessons != null)
            {
                foreach (var lesson in quiz.Lessons)
                {
                    quizLessons.Add(await _unitOfWork.QuizLessonRespo.GetAsync(lesson, ManagerAddress.domain, ManagerAddress.quizLesson));
                }
                ViewBag.QuizLessons = quizLessons;
                return View(quiz);
            }
            return View();
        }*/
    }
}
