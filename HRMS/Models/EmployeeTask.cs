namespace HRMS.Models
{
    public class EmployeeTask
    {
        public int Id { get; set; }
        public string TaskName { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime? CompletedOn { get; set; }
    }

}
