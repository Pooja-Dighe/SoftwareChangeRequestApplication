using System.ComponentModel.DataAnnotations;

namespace SCRSApplication.Models
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string EmpID { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Project { get; set; }
    }
}
