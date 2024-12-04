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

        //[DataType(DataType.Date)] // Specifies the input type as a date picker
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)] // Formats the date
        public DateOnly? DueDate { get; set; }              // by requester
        public string? Project { get; set; }
        public string? RequestStatus { get; set; }               // approved or rejected
        public string? WorkStatus { get; set; }            // Ongoing or Completed 
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }               // Foreign key for IdentityUser 
        public string? RoleId { get; set; }
        public IdentityRole Role { get; set; }               // Foreign key for IdentityRole
        
        public string? Comments { get; set; }   
       // public int? TimelineId { get; set; }           // working time for developers      
    }
}
