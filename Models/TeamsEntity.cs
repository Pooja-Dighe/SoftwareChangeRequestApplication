using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SCRSApplication.Models
{
    public class TeamsEntity
    {
        [Key]
        public int Id { get; set; }
        public string? UserId{ get; set; }
        public IdentityUser User { get; set; }

        public int? ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
    }
}
