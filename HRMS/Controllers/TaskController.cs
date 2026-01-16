using HRMS.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [AuthorizeRole("Employee")]
    public class TaskController : Controller
    {
        // 🟢 Employee Daily Tasks
        public IActionResult Index()
        {
            return View();
        }
    }
}
