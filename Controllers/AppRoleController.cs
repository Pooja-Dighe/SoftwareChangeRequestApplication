using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SCRSApplication.Controllers
{
    [Authorize]
    public class AppRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppRoleController(RoleManager<IdentityRole> roleManger)
        {
            _roleManager = roleManger;
        }
        //List all roles created by user
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {    
                return View();   
        }

        [HttpPost]
        public async Task<IActionResult>Create(IdentityRole model)
        {
            if(!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())   //avoid duplicate roles
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }

    }
}
