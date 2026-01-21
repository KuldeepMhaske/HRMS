using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string First_Name { get; set; }

        [Required]
        public string Last_Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string EmpCode { get; set; }
        public string Address { get; set; }
        public string Emergency_No { get; set; }
        public string Blood_Group { get; set; }

        // 🔐 AUTH
        [Required]
        public string PasswordHash { get; set; }

        public bool IsActive { get; set; } = true;

        // 🔗 ROLE FOREIGN KEY
        [Required]
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        // 🔗 ADMIN WHO CREATED THIS EMPLOYEE
        [Required]
        public int CreatedByAdminId { get; set; }
    }
}
