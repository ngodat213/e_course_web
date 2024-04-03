using e_course_web.Manager;
using e_course_web.Models;
using e_course_web.Repositorys;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class QuestionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Quiz> quizs = _unitOfWork.Quiz.GetAll();
            if (quizs != null)
            {
                return View(quizs);
            }
            return View();
        }

        public async Task<IActionResult> QuestionDetail(int id)
        {
            Quiz quiz = await _unitOfWork.Quiz.GetById(id);
            if (quiz != null)
            {
                return View(quiz);
            }
            return NotFound();
        }
        // Code !!!!
        public async Task<IActionResult> QuestionPlay(int id)
        {
            Quiz quiz = await _unitOfWork.Quiz.GetById(id);
            if (quiz != null)
            {
                return View(quiz);
            }
            return NotFound();
        }
    }
}
