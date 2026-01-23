using HRMS.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[AuthorizeRole("Employee")]
public class LeaveController : Controller
{
    public IActionResult Index(int? month, int? year)
    {
        DateTime today = DateTime.Today;

        int selectedMonth = month ?? today.Month;
        int selectedYear = year ?? today.Year;

        ViewBag.SelectedMonth = selectedMonth;
        ViewBag.SelectedYear = selectedYear;

        ViewBag.Months = Enumerable.Range(1, 12)
            .Select(m => new SelectListItem
            {
                Value = m.ToString(),
                Text = new DateTime(2000, m, 1).ToString("MMMM"),
                Selected = (m == selectedMonth)
            }).ToList();

        ViewBag.Years = Enumerable.Range(today.Year - 5, 11)
            .Select(y => new SelectListItem
            {
                Value = y.ToString(),
                Text = y.ToString(),
                Selected = (y == selectedYear)
            }).ToList();

        return View();
    }
}
