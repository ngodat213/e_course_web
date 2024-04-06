using e_course_web.DataAccess.Repositorys;
using e_course_web.Helpers;
using e_course_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            CookieHelper.SetOrRemoveCookie(Response.Cookies, "", "", 3);
            IEnumerable<Blog> blogAll =  _unitOfWork.Blog.GetAll();
            if (blogAll != null)
            {
                List<Blog> blogs = new List<Blog>();
                foreach (var blog in blogAll)
                {
                    if (blog.Type)
                    {
                        blogs.Add(blog);
                    }
                }
                return View(blogs);
            }
            return View();
        }

        public async Task<IActionResult> BlogDetail(int id)
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
