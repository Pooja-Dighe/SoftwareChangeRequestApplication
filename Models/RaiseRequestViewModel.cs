using System.ComponentModel.DataAnnotations;

namespace SCRSApplication.Models
{
    public class UserViewModel 
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }


        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public string? Priority { get; set; }


        [Required]
        [Display(Name = "Due Date")]
        public DateOnly DueDate { get; set; }

        [Required]
        [Display(Name = "Project")]
        public string? Project { get; set; }

        [Required]
        [Display(Name = "UserId")]
        public string? UserId { get; set; }

        [Required]
        [Display(Name = "RoleId")]
        public string? RoleId { get; set; }


    }
}
