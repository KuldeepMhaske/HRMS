using HRMS.Data;
using HRMS.Filters;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;

[AuthorizeRole("Master")]
public class AdminTaskController : Controller
{
    private readonly AppDbContext _context;

    public AdminTaskController(AppDbContext context)
    {
        _context = context;
    }

    // ================= ASSIGN TASK (GET) =================
    public IActionResult Assign()
    {
        ViewBag.Employees = _context.Employees
                                    .Where(e => e.IsActive)
                                    .ToList();
        return View();
    }

    // ================= ASSIGN TASK (POST) =================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Assign(int employeeId, string title, string description)
    {
        if (employeeId == 0 || string.IsNullOrWhiteSpace(title))
        {
            TempData["Error"] = "Employee and Task Title are required";
            return RedirectToAction(nameof(Assign));
        }

        int adminId = HttpContext.Session.GetInt32("AdminId")!.Value;

        var task = new DailyTask
        {
            EmployeeId = employeeId,
            Title = title,
            Description = description,
            TaskDate = DateTime.Now,
            AssignedById = adminId,
            AssignedBy = "Admin", // optional (can be removed later)
            Status = "Pending"
        };
        TempData["Success"] = "Task assigned successfully";
        _context.DailyTasks.Add(task);
        _context.SaveChanges();

        //TempData["Success"] = "Task assigned successfully";
        return RedirectToAction("Employees", "Admin");
    }
}
