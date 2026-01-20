using HRMS.Data;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace HRMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ================= LOGIN (GET) =================
        [HttpGet]
        public IActionResult Login()
        {
            var authType = HttpContext.Session.GetString("AuthType");

            if (authType == "Master")
                return RedirectToAction("Dashboard", "Admin");

            if (authType == "Employee")
                return RedirectToAction("Dashboard", "Employee");

            return View();
        }

        // ================= LOGIN (POST) =================
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password are required";
                return View();
            }

            string hash = HashPassword(password);

            var appUser = _context.AppUsers
                .FirstOrDefault(x => x.Username == username && x.PasswordHash == hash);

            if (appUser != null)
            {
                HttpContext.Session.SetString("AuthType", "Master");
                HttpContext.Session.SetString("Username", appUser.Username);
                HttpContext.Session.SetString("UserRole", appUser.Role);
                HttpContext.Session.SetInt32("AdminId", appUser.Id);

                return RedirectToAction("Dashboard", "Admin");
            }

            var employee = _context.Employees
                .FirstOrDefault(x => x.Email == username && x.PasswordHash == hash);

            if (employee != null)
            {
                if (!employee.IsActive)
                {
                    ViewBag.Error = "Your account is deactivated.";
                    return View();
                }

                HttpContext.Session.SetString("AuthType", "Employee");
                HttpContext.Session.SetInt32("EmployeeId", employee.Id);
                HttpContext.Session.SetString("Username", employee.First_Name);
                HttpContext.Session.SetString("UserRole", employee.Role);

                return RedirectToAction("Dashboard", "Employee");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        // ================= SIGNUP =================
        [HttpGet]
        public IActionResult Signup()
        {
            if (HttpContext.Session.GetString("AuthType") == "Master")
                return RedirectToAction("Dashboard", "Admin");

            return View();
        }

        [HttpPost]
        public IActionResult Signup(string username, string password, string role, string licenseKey)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(role))
            {
                ViewBag.Error = "All fields are required";
                return View();
            }

            // 🔐 LICENSE CHECK
            if (role == "Admin" || role == "HR")
            {
                var validLicense = _configuration["AdminLicenseKey"];

                if (licenseKey != validLicense)
                {
                    ViewBag.Error = "Invalid License Key";
                    return View();
                }
            }

            if (_context.AppUsers.Any(x => x.Username == username))
            {
                ViewBag.Error = "Username already exists";
                return View();
            }

            var user = new AppUser
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Role = role
            };

            _context.AppUsers.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Profile created successfully";
            return RedirectToAction("Login");
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
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
