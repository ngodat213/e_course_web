using e_course_web.Helpers;
using e_course_web.Manager;
using e_course_web.Models;
using e_course_web.Repository;
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
        public async Task<IActionResult> Index()
        {

            CookieHelper.SetOrRemoveCookie(Response.Cookies, "", "", 3);
            BlogResponse blogResponse = await _unitOfWork.BlogRespo.GetAsync(ManagerAddress.domain, ManagerAddress.blog);
            if(blogResponse != null)
            {
                List<Blog> blogs = new List<Blog>();
                foreach(var blog in blogResponse.blogs)
                {
                    if (blog.type)
                    {
                        blogs.Add(blog);
                    }
                }
                return View(blogs);
            }
            return View();
        }
        
        public async Task<IActionResult> BlogDetail(string id)
        {
            Blog blog = await _unitOfWork.BlogRespo.GetAsync(id, ManagerAddress.domain, ManagerAddress.blog);
            if (blog != null)
            {
                return View(blog);
            }
            return View();
        }
    }
}
