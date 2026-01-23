using HRMS.Data;
using HRMS.Filters;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;

[AuthorizeRole("Employee")]
public class EmployeeTaskController : Controller
{
    private readonly AppDbContext _context;

    public EmployeeTaskController(AppDbContext context)
    {
        _context = context;
    }

    // ================= MY TASKS =================
    public IActionResult Index()
    {
        int empId = HttpContext.Session.GetInt32("EmployeeId")!.Value;

        var tasks = _context.DailyTasks
            .Where(t => t.EmployeeId == empId)
            .OrderByDescending(t => t.TaskDate)
            .ToList();

        return View(tasks);
    }

    // ================= CREATE TASK (GET) =================
    public IActionResult Create()
    {
        return View();
    }

    // ================= CREATE TASK (POST) =================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            ModelState.AddModelError("", "Task title is required");
            return View();
        }

        int empId = HttpContext.Session.GetInt32("EmployeeId")!.Value;

        var task = new DailyTask
        {
            EmployeeId = empId,
            Title = title,
            Description = description,
            TaskDate = DateTime.Now,
            Status = "Pending",
            AssignedBy = "Employee",
            AssignedById = empId
        };

        _context.DailyTasks.Add(task);
        _context.SaveChanges();

        TempData["Success"] = "Task added successfully";
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public IActionResult Complete(int id)
    {
        var task = _context.DailyTasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return NotFound();
        }

        task.Status = "Completed";
        task.TaskDate = DateTime.Now; // optional

        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
