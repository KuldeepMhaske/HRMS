using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required, MaxLength(20)]
        public string Role { get; set; } // Admin / HR
    }
}
