using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
using e_course_web.Service.Helpers;
using e_course_web.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Teacher)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // Controller: IUnitOfWork
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var cateGetList = _unitOfWork.Categories.GetAll();
            return View(cateGetList);
        }

        public async Task<IActionResult> Create(int? id)
        {
            Categories categories = new Categories();
            if(id != null)
            {
                categories = await _unitOfWork.Categories.GetById(id);
                return View(categories);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? id, Categories categories)
        {
            if(id == null)
            {
                _unitOfWork.Categories.Add(new Categories() { Category = categories.Category});
            }
            else
            {
                _unitOfWork.Categories.Update(categories);
            }
            return View();
        }
    }
}
