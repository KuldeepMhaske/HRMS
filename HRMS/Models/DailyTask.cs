using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class DailyTask
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime TaskDate { get; set; }

        public string Status { get; set; } = "Pending";

        // 🔥 NEW
        public string AssignedBy { get; set; }  // Admin / Employee
        public int AssignedById { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Employee Employee { get; set; }
    }
}