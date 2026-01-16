using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string EmpCode { get; set; }
        public string Address { get; set; }
        public string Emergency_No { get; set; }
        public string Blood_Group { get; set; }

        public string Role { get; set; }
        public string PasswordHash { get; set; }

        // ✅ NEW FIELD
        public bool IsActive { get; set; } = true;
    }

}
