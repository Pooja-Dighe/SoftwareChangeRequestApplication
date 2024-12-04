using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCRSApplication.Data;
using SCRSApplication.Models;

namespace SCRSApplication.Controllers
{
    public class RaiseRequestController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RaiseRequestController(ApplicationDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }
    
        // GET: RaiseRequest
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.RaiseRequestEntity.Include(r => r.Role).Include(r => r.User);
            var appuser = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(appuser);

            ViewData["UserId"] = appuser.Id;
            ViewData["UserName"] = appuser.UserName;

            ViewData["Roles"] = roles;
            return View(await applicationDBContext.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            var appuser = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(appuser);

            var roleName = roles.FirstOrDefault();
            var roleId = string.Empty;

            if (roleName != null)
            {
                roleId = _context.Roles
                    .Where(r => r.Name == roleName)
                    .Select(r => r.Id)
                    .FirstOrDefault();
            }

            ViewData["UserId"] = appuser.Id;
            ViewData["UserName"] = appuser.UserName;
            ViewData["RoleId"] = roleId; // Pass RoleId
            ViewData["Roles"] = roles;
            return View();
        }

        // GET: RaiseRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                RaiseRequestEntity raiseRequestEntity = new RaiseRequestEntity
                {
                    Title = userViewModel.Title,
                    Description = userViewModel.Description,
                    Priority = userViewModel.Priority,
                    DueDate = userViewModel.DueDate,
                    Project = userViewModel.Project,
                    RoleId = userViewModel.RoleId,
                    UserId = userViewModel.UserId
                };

                  _context.RaiseRequestEntity.Add(raiseRequestEntity);
                  _context.SaveChanges();
                  return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

    }
}
