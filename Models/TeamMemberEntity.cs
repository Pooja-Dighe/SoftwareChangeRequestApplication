using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SCRSApplication.Models
{
    public class TeamMemberEntity
    {
        [Key]
        public int Id { get; set; }
        public string? MemberId { get; set; }
        public IdentityUser User { get; set; }
        public string? Position { get; set; }

        public int? TeamId { get; set; }
        public TeamsEntity Team { get; set; }
    }
}
