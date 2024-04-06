using e_course_web.DataAccess.Repositorys;
using e_course_web.Manager;
using e_course_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class QAController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public QAController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Blog> blogs = _unitOfWork.Blog.GetAll();
            if (blogs != null)
            {
                List<Blog> qAs = new List<Blog>();
                foreach (var qA in blogs)
                {
                    if (qA.Type!)
                    {
                        qAs.Add(qA);
                    }
                }
                return View(qAs);
            }
            return View();
        }
        public async Task<IActionResult> QADetail(int id)
        {
            Blog blog = await _unitOfWork.Blog.GetById(id);
            if (blog != null)
            {
                return View(blog);
            }
            return View();
        }
    }
}
