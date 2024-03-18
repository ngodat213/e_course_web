using e_course_web.Models;
using Microsoft.AspNetCore.Mvc;
using e_course_web.DataQuery;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;

namespace e_course_web.Controllers
{
    public class ContactController : Controller
    {
        // GET
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
