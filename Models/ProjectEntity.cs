using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SCRSApplication.Models
{
    public class ProjectEntity 
    {
        [Key]
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TeamId { get; set; }
        public TeamsEntity Teams { get; set; }
       

    }




    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public string? UserId { get; set; }
        public string? User { get; set; }

    }
}
