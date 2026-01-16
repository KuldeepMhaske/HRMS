using HRMS.Data;
using HRMS.Filters;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [AuthorizeRole("Employee")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // ================= EMPLOYEE SELF PROFILE =================
        [HttpGet]
        public IActionResult MyProfile()
        {
            int empId = HttpContext.Session.GetInt32("EmployeeId")!.Value;

            var employee = _context.Employees.Find(empId);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MyProfile(Employee model)
        {
            int empId = HttpContext.Session.GetInt32("EmployeeId")!.Value;

            if (model.Id != empId)
                return Unauthorized();

            var employee = _context.Employees.Find(empId);
            if (employee == null)
                return NotFound();

            // ✅ Employee can update ONLY safe fields
            employee.First_Name = model.First_Name;
            employee.Last_Name = model.Last_Name;
            employee.Address = model.Address;

            _context.SaveChanges();

            ViewBag.Success = "Profile updated successfully";
            return View(employee);
        }
    }
}
