using e_course_web.Manager;
using e_course_web.Models;
using e_course_web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    public class QAController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public QAController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            BlogResponse blogResponse = await _unitOfWork.BlogRespo.GetAsync(ManagerAddress.domain, ManagerAddress.blog);
            if (blogResponse == null)
            {
                List<Blog> qAs = new List<Blog>();
                foreach (var qA in blogResponse.blogs)
                {
                    if (qA.type!)
                    {
                        qAs.Add(qA);
                    }
                }
                return View(qAs);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string id)
        {
            Blog blog = await _unitOfWork.BlogRespo.GetAsync(id, ManagerAddress.domain, ManagerAddress.blog);
            if (blog == null)
            {
                return View(blog);
            }
            return View();
        }
    }
}
