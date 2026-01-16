using HRMS.Data;
using HRMS.Filters;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
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
            ViewBag.TotalEmployees = _context.Employees.Count();
            ViewBag.ActiveEmployees = _context.Employees.Count(e => e.IsActive);
            ViewBag.InactiveEmployees = _context.Employees.Count(e => !e.IsActive);

            return View();
        }

        // ================= EMPLOYEE LIST =================
        public IActionResult Employees(string search)
        {   
            var employees = _context.Employees.AsQueryable();

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

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return RedirectToAction("Employees");
        }

        // ================= EDIT EMPLOYEE =================
        public IActionResult EditEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmployee(Employee model, string NewPassword)
        {
            if (!ModelState.IsValid)
                return View(model);

            var emp = _context.Employees.Find(model.Id);
            if (emp == null)
                return NotFound();

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
            var employee = _context.Employees.Find(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // ================= DELETE EMPLOYEE =================
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmployeeConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Employees));
        }

        // ================= ACTIVATE / DEACTIVATE =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
                return NotFound();

            emp.IsActive = !emp.IsActive;
            _context.SaveChanges();

            return RedirectToAction(nameof(Employees));
        }

        // ================= RESET EMPLOYEE PASSWORD =================
        public IActionResult ResetEmployeePassword(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
                return NotFound();

            ViewBag.EmployeeName = $"{employee.First_Name} {employee.Last_Name}";
            ViewBag.EmployeeId = employee.Id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetEmployeePassword(int id, string NewPassword)
        {
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                ViewBag.Error = "Password cannot be empty";
                ViewBag.EmployeeId = id;
                return View();
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
                return NotFound();

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
