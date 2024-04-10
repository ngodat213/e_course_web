using Microsoft.AspNetCore.Mvc;
using e_course_web.DataAccess.Repositorys;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public TeacherController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       /* public IActionResult Index()
        {
            var teacher = _unitOfWork.
            return View();
        }*/
    }
}
