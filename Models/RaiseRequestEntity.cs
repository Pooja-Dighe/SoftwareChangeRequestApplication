using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SCRSApplication.Models
{
    public class RaiseRequestEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Title {  get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public DateTime DueDate { get; set; }              // by requester
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
        public string? RequestStatus { get; set; }               // approved or rejected
        public string? WorkStatus { get; set; }            // Ongoing or Completed 
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }               // Foreign key for IdentityUser 
        public string? RoleId { get; set; }
        public IdentityRole Role { get; set; }               // Foreign key for IdentityRole

        public DateTime AddedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }       // Maps to SQL timestamp/rowversion
        public string? Comments { get; set; }  

        public int? TeamMemberId { get; set; }
        public TeamMemberEntity TeamMember { get; set; }
       // public int? TimelineId { get; set; }           // working time for developers      
    }
}
