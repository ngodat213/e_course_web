using e_course_web.DataAccess.DbContext;
using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
using e_course_web.Service.Helpers;
using e_course_web.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ApplicationDbContext _db;

        public UserController(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<User> userList = _unitOfWork.User.GetAll();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            foreach(var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }
            return View(userList);
        }
        // Lock user
        [HttpPost]
        public IActionResult Index(string id)
        {
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _unitOfWork.User.Update(user);
            return RedirectToAction(nameof(Index)); ;
        }
    }
}
