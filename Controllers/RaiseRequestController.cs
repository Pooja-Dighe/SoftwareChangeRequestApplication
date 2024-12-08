using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCRSApplication.Data;
using SCRSApplication.Models;

namespace SCRSApplication.Controllers
{
    [Authorize]
    public class RaiseRequestController : BaseController
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RaiseRequestController(ApplicationDBContext context, UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager) : base(userManager, roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
    
        // GET: RaiseRequest
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.RaiseRequestEntity.Include(r => r.Role).Include(r => r.User);      
            return View(await applicationDBContext.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            var userDetails = await GetUserDetailsAsync();

            ViewBag.UserId = userDetails.UserId;
            ViewBag.UserName = userDetails.UserName;
            ViewBag.RoleId = userDetails.RoleId;
            ViewBag.RoleName = userDetails.RoleName;
            
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userDetails = await GetUserDetailsAsync();

            ViewBag.UserId = userDetails.UserId;
            ViewBag.UserName = userDetails.UserName;
            ViewBag.RoleId = userDetails.RoleId;
            ViewBag.RoleName = userDetails.RoleName;
            var request = _context.RaiseRequestEntity.FirstOrDefault(r => r.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel userViewModel) 
        {
            if(id != userViewModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            {
                try
                {
                    RaiseRequestEntity raiseRequestEntity = new RaiseRequestEntity
                    {
                        Id = userViewModel.Id,
                        Title = userViewModel.Title,
                        Description = userViewModel.Description,
                        Priority = userViewModel.Priority,
                        DueDate = userViewModel.DueDate,
                        Project = userViewModel.Project,
                        RoleId = userViewModel.RoleId,
                        UserId = userViewModel.UserId
                    };

                    _context.RaiseRequestEntity.Update(raiseRequestEntity);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateConcurrencyException) 
                {
                    if (!_context.RaiseRequestEntity.Any(p => p.Id == id))
                    {
                        return NotFound(); // Handle cases where the record was deleted
                    }
                    throw; // Re-throw the exception if it's not concurrency related

                }
            }
            return View(userViewModel);
        }

        public async Task<IActionResult>Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var raiseRequestEntity = await _context.RaiseRequestEntity
                .Include(r => r.Role)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raiseRequestEntity == null)
            {
                return NotFound();
            }

            return View(raiseRequestEntity);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var raiseRequestEntity = await _context.RaiseRequestEntity
                .Include(r => r.Role)
                .Include(r => r.User).FirstOrDefaultAsync(m => m.Id == id);
            if (raiseRequestEntity == null)
            { 
                return NotFound();
            }

            return View(raiseRequestEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var raiseRequestEntity = await _context.RaiseRequestEntity.FindAsync(id);
            if(raiseRequestEntity != null)
            {
                _context.RaiseRequestEntity.Remove(raiseRequestEntity);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
