using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
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
            IEnumerable<Exam> quizs = _unitOfWork.Exam.GetAll();
            if (quizs != null)
            {
                return View(quizs);
            }
            return View();
        }

        public async Task<IActionResult> QuestionDetail(int id)
        {
            Exam quiz = await _unitOfWork.Exam.GetById(id);
            if (quiz != null)
            {
                return View(quiz);
            }
            return NotFound();
        }
        // Code !!!!
        public async Task<IActionResult> QuestionPlay(int id)
        {
            Exam quiz = await _unitOfWork.Exam.GetById(id);
            if (quiz != null)
            {
                return View(quiz);
            }
            return NotFound();
        }
    }
}
