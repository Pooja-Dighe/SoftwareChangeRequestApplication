using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SCRSApplication.Models
{
    public class TeamsEntity
    {
        [Key]
        public int Id { get; set; }
        public string? TeamName{ get; set; }

    }
}
