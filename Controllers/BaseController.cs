using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SCRSApplication.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
  
        public string RoleId { get; private set; }
        public string UserId { get; private set; }
        public BaseController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected async Task<(string UserId, string UserName, string RoleId, string RoleName)> GetUserDetailsAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get UserId and UserName
                var user = await _userManager.GetUserAsync(User);
                var userId = user?.Id;
                var userName = user?.UserName;

                // Get RoleId and RoleName
                var roles = await _userManager.GetRolesAsync(user);
                var roleName = roles.FirstOrDefault(); // Assuming the user has only one role

                string roleId = null;
                if (!string.IsNullOrEmpty(roleName))
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    roleId = role?.Id;
                }

                return (userId, userName, roleId, roleName);
            }

            return (null, null, null, null);
        }


        protected List<SelectListItem> GetPriorityDropdownValues()
        {
            return new List<SelectListItem>
            {
              new SelectListItem { Text = "Low", Value = "1" },
              new SelectListItem { Text = "Medium", Value = "2" },
              new SelectListItem { Text = "High", Value = "3" }
            };
        }
    }
}
