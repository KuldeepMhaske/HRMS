using HRMS.Data;
using HRMS.Filters;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace HRMS.Controllers
{
    [AuthorizeRole("Master")] // 🔐 Only Admin / Master
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // ================= DASHBOARD =================
        public IActionResult Dashboard()
        {
            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

            ViewBag.TotalEmployees = _context.Employees
                .Count(e => e.CreatedByAdminId == adminId);

            ViewBag.ActiveEmployees = _context.Employees
                .Count(e => e.CreatedByAdminId == adminId && e.IsActive);

            ViewBag.InactiveEmployees = _context.Employees
                .Count(e => e.CreatedByAdminId == adminId && !e.IsActive);

            return View();
        }

        // ================= EMPLOYEE LIST =================
        public IActionResult Employees(string search)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

            var employees = _context.Employees
                .Where(e => e.CreatedByAdminId == adminId)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                employees = employees.Where(e =>
                    e.EmpCode.Contains(search) ||
                    e.First_Name.Contains(search) ||
                    e.Last_Name.Contains(search) ||
                    e.Email.Contains(search)
                );
            }

            ViewBag.Search = search;
            return View(employees.ToList());
        }

        // ================= CREATE EMPLOYEE =================
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmployee(Employee employee, string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ModelState.AddModelError("", "Password is required");
                return View(employee);
            }

            employee.Id = _context.Employees.Any()
                ? _context.Employees.Max(e => e.Id) + 1
                : 1;

            employee.PasswordHash = HashPassword(Password);
            employee.IsActive = true;
            employee.Role = "Employee";

            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;
            employee.CreatedByAdminId = adminId;

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return RedirectToAction(nameof(Employees));
        }

        // ================= EDIT EMPLOYEE =================
        public IActionResult EditEmployee(int id)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

            var employee = _context.Employees
                .FirstOrDefault(e => e.Id == id && e.CreatedByAdminId == adminId);

            if (employee == null)
                return Unauthorized();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmployee(Employee model, string NewPassword)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

            var emp = _context.Employees
                .FirstOrDefault(e => e.Id == model.Id && e.CreatedByAdminId == adminId);

            if (emp == null)
                return Unauthorized();

            emp.First_Name = model.First_Name;
            emp.Last_Name = model.Last_Name;
            emp.Email = model.Email;
            emp.Phone = model.Phone;
            emp.EmpCode = model.EmpCode;
            emp.Address = model.Address;
            emp.Emergency_No = model.Emergency_No;
            emp.Blood_Group = model.Blood_Group;

            if (!string.IsNullOrWhiteSpace(NewPassword))
                emp.PasswordHash = HashPassword(NewPassword);

            _context.SaveChanges();
            return RedirectToAction(nameof(Employees));
        }

        // ================= EMPLOYEE DETAILS =================
        public IActionResult EmployeeDetails(int id)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

            var employee = _context.Employees
                .FirstOrDefault(e => e.Id == id && e.CreatedByAdminId == adminId);

            if (employee == null)
                return Unauthorized();

            return View(employee);
        }

        // ================= DELETE EMPLOYEE =================
        public IActionResult DeleteEmployee(int id)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

            var employee = _context.Employees
                .FirstOrDefault(e => e.Id == id && e.CreatedByAdminId == adminId);

            if (employee == null)
                return Unauthorized();

            return View(employee);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmployeeConfirmed(int id)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

            var employee = _context.Employees
                .FirstOrDefault(e => e.Id == id && e.CreatedByAdminId == adminId);

            if (employee == null)
                return Unauthorized();

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction(nameof(Employees));
        }

        // ================= ACTIVATE / DEACTIVATE =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

            var emp = _context.Employees
                .FirstOrDefault(e => e.Id == id && e.CreatedByAdminId == adminId);

            if (emp == null)
                return Unauthorized();

            emp.IsActive = !emp.IsActive;
            _context.SaveChanges();

            return RedirectToAction(nameof(Employees));
        }

        // ================= RESET EMPLOYEE PASSWORD =================
        public IActionResult ResetEmployeePassword(int id)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

            var employee = _context.Employees
                .FirstOrDefault(e => e.Id == id && e.CreatedByAdminId == adminId);

            if (employee == null)
                return Unauthorized();

            ViewBag.EmployeeName = $"{employee.First_Name} {employee.Last_Name}";
            ViewBag.EmployeeId = employee.Id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetEmployeePassword(int id, string NewPassword)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

            var employee = _context.Employees
                .FirstOrDefault(e => e.Id == id && e.CreatedByAdminId == adminId);

            if (employee == null)
                return Unauthorized();

            employee.PasswordHash = HashPassword(NewPassword);
            _context.SaveChanges();

            TempData["Success"] = "Password reset successfully";
            return RedirectToAction(nameof(Employees));
        }

        // ================= HASH =================
        private string HashPassword(string password)
        {
            using SHA256 sha = SHA256.Create();
            return Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(password))
            );
        }
    }
}
