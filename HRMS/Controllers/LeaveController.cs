using HRMS.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [AuthorizeRole("Employee")]
    public class LeaveController : Controller
    {
        // 🟢 Employee Leave Dashboard
        public IActionResult Index()
        {
            return View();
        }
    }
}
