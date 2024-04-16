using e_course_web.DataAccess.Repositorys;
using e_course_web.ViewModels;
using e_course_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Expert")]
    public class TeacherController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TeacherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("")]
        public IActionResult Index(string id)
        {
            var user = _unitOfWork.User.GetFirstOrDefault(p=> p.Id == id);
            var courses = _unitOfWork.Course.GetAll(p=> p.TeacherId == id);
            TeacherVM teacherVM = new TeacherVM() { 
                PhotoUrl = user.PhotoUrl,
                Courses = courses,
                Description = user.Description,
                Expert = user.Expert,
                FullName = user.FullName,
                Introduce = user.Introduce,
            };
            return View(teacherVM);
        }
    }
}
