using Microsoft.AspNetCore.Mvc;
using LeavePortal.Models;
using System;

namespace LeavePortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(Users model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.PasswordHash) || string.IsNullOrWhiteSpace(model.Role))
            {
                ViewBag.Error = "Please fill all fields.";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u =>
                       u.Username.ToLower() == model.Username.ToLower() &&
                       u.PasswordHash == model.PasswordHash &&
                       u.Role.ToLower() == model.Role.ToLower());


            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    return RedirectToAction("AdminDashboard", "Leave");
                else
                    return RedirectToAction("EmployeeDashboard", "Leave");
            }

            ViewBag.Error = "Invalid Credentials.";
            return View();
        }
    }
}
