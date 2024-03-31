using e_course_web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class TeacherController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public TeacherController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
