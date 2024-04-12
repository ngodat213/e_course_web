using e_course_web.DataAccess.Repositorys;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Exam")]
    public class ExamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExamController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        [Route("detail")]
        public IActionResult Detail()
        {
            return View();
        }

        [Route("test")]
        public IActionResult Test()
        {
            return View();
        }
    }
}
