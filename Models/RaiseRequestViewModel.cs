using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SCRSApplication.Models
{
    public class RaiseRequestViewModel 
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string? Title { get; set; }


        [Required]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public string? PriorityValue { get; set; }  //value
        public string? Priority { get; set; }     //text
        public IEnumerable<SelectListItem> PriorityList { get; set; } = new List<SelectListItem>();


        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
     
        [Required]
        [Display(Name = "Project")]
        public string? Project { get; set; }

        [Required]
        [Display(Name = "UserId")]
        public string? UserId { get; set; }

        [Required]
        [Display(Name = "RoleId")]
        public string? RoleId { get; set; }

        public string? Comments { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
