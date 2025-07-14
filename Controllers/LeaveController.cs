using Microsoft.AspNetCore.Mvc;
using LeavePortal.Models;

namespace LeavePortal.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult EmployeeDashboard()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var leaves = _context.LeaveRequests.Where(l => l.UserId == userId).ToList();
            return View(leaves);
        }

        [HttpGet]
        public IActionResult ApplyLeave() => View();

        [HttpPost]
        public IActionResult ApplyLeave(LeaveRequest request)
        {
            request.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            _context.LeaveRequests.Add(request);
            _context.SaveChanges();
            return RedirectToAction("EmployeeDashboard");
        }

        public IActionResult AdminDashboard()
        {
            var leaves = _context.LeaveRequests.ToList();
            return View(leaves);
        }

        public IActionResult Approve(int id)
        {
            var leave = _context.LeaveRequests.Find(id);
            if (leave != null)
            {
                leave.Status = "Approved";
                _context.SaveChanges();
            }
            return RedirectToAction("AdminDashboard");
        }

        public IActionResult Reject(int id)
        {
            var leave = _context.LeaveRequests.Find(id);
            if (leave != null)
            {
                leave.Status = "Rejected";
                _context.SaveChanges();
            }
            return RedirectToAction("AdminDashboard");
        }
    }
}

