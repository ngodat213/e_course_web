using e_course_web.DataAccess.DbContext;
using e_course_web.DataAccess.Repositorys;
using e_course_web.Models;
using e_course_web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            return View();
        }

        public IActionResult Lock(string id)
        {
            var objFromDb = _unitOfWork.User.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                // user is currently locked, will unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            return RedirectToAction(nameof(Index)); ;
        }
    }
}
