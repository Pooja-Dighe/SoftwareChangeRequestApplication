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
            var userDetails = await GetUserDetailsAsync();

            ViewBag.UserId = userDetails.UserId;
            ViewBag.UserName = userDetails.UserName;
            ViewBag.RoleId = userDetails.RoleId;
            ViewBag.RoleName = userDetails.RoleName;

            var PriorityValues = GetPriorityDropdownValues();
            RaiseRequestEntity raiseRequestEntity = new RaiseRequestEntity();

            foreach (var entity in applicationDBContext)
            {
                entity.Priority = PriorityValues.FirstOrDefault(d => d.Value == raiseRequestEntity.Priority)?.Text;
            }
            return View(await applicationDBContext.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            var userDetails = await GetUserDetailsAsync();

            ViewBag.UserId = userDetails.UserId;
            ViewBag.UserName = userDetails.UserName;
            ViewBag.RoleId = userDetails.RoleId;
            ViewBag.RoleName = userDetails.RoleName;
  
            ViewBag.PriorityList = GetPriorityDropdownValues();
            return View();
        }

        // GET: RaiseRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(RaiseRequestViewModel userViewModel)
        {
           
            if (ModelState.IsValid)
            {
                var PriorityValues = GetPriorityDropdownValues();
                RaiseRequestEntity raiseRequestEntity = new RaiseRequestEntity
                {
                    Title = userViewModel.Title,
                    Description = userViewModel.Description,
                    Priority = userViewModel.PriorityValue,
                    DueDate = userViewModel.DueDate,
                    Project = userViewModel.Project,
                    RoleId = userViewModel.RoleId,
                    UserId = userViewModel.UserId,
                    AddedAt = DateTime.Now,
                };

                  _context.RaiseRequestEntity.Add(raiseRequestEntity);
                  _context.SaveChanges();
                  return RedirectToAction(nameof(Index));
            }
            ViewBag.PriorityList = GetPriorityDropdownValues();
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
            var raiseRequestEntity = _context.RaiseRequestEntity.FirstOrDefault(r => r.Id == id);
            if (raiseRequestEntity == null)
            {
                return NotFound();
            }
          
            var  userViewModel = new RaiseRequestViewModel
            {
                Id = raiseRequestEntity.Id,
                Title = raiseRequestEntity.Title,
                Description = raiseRequestEntity.Description,
                PriorityValue = raiseRequestEntity.Priority,
                DueDate = raiseRequestEntity.DueDate,
                Project = raiseRequestEntity.Project,
                RoleId = raiseRequestEntity.RoleId,
                UserId = raiseRequestEntity.UserId,
                UpdatedAt = DateTime.Now,
            };

            userViewModel.PriorityList = GetPriorityDropdownValues();
            return View(userViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RaiseRequestViewModel userViewModel) 
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
                        Priority = userViewModel.PriorityValue,
                        DueDate = userViewModel.DueDate,
                        Project = userViewModel.Project,
                        RoleId = userViewModel.RoleId,
                        UserId = userViewModel.UserId,
                        UpdatedAt = userViewModel.UpdatedAt
                    };

                    _context.RaiseRequestEntity.Update(raiseRequestEntity);
                    _context.SaveChanges();
                    ViewBag.PriorityList = GetPriorityDropdownValues();
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
            var userDetails = await GetUserDetailsAsync();

            ViewBag.UserId = userDetails.UserId;
            ViewBag.UserName = userDetails.UserName;
            ViewBag.RoleId = userDetails.RoleId;
            ViewBag.RoleName = userDetails.RoleName;
            if (id == null)
            {
                return NotFound();
            }
            var raiseRequestEntity = await _context.RaiseRequestEntity
                .Include(r => r.Role)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            var PriorityValues = GetPriorityDropdownValues();

            var raiseRequestViewModel = new RaiseRequestViewModel
            {
                Id = raiseRequestEntity.Id,
                Title = raiseRequestEntity.Title,
                Description = raiseRequestEntity.Description,
                PriorityValue = raiseRequestEntity.Priority,
                Priority = PriorityValues.FirstOrDefault(d => d.Value == raiseRequestEntity.Priority)?.Text,
                DueDate = raiseRequestEntity.DueDate,
                Project = raiseRequestEntity.Project,
                Comments = raiseRequestEntity.Comments
            };

            if (raiseRequestViewModel == null)
            {
                return NotFound();
            }
            return View(raiseRequestViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var userDetails = await GetUserDetailsAsync();

            ViewBag.UserId = userDetails.UserId;
            ViewBag.UserName = userDetails.UserName;
            ViewBag.RoleId = userDetails.RoleId;
            ViewBag.RoleName = userDetails.RoleName;
            if (id == null)
            {
                return NotFound();
            }
            var raiseRequestEntity = await _context.RaiseRequestEntity
                .Include(r => r.Role)
                .Include(r => r.User).FirstOrDefaultAsync(m => m.Id == id);

            var PriorityValues = GetPriorityDropdownValues();

            var raiseRequestViewModel = new RaiseRequestViewModel
            {
                Id = raiseRequestEntity.Id,
                Title = raiseRequestEntity.Title,
                Description = raiseRequestEntity.Description,
                PriorityValue = raiseRequestEntity.Priority,
                Priority = PriorityValues.FirstOrDefault(d => d.Value == raiseRequestEntity.Priority)?.Text,
                DueDate = raiseRequestEntity.DueDate,
                Project = raiseRequestEntity.Project,
                Comments = raiseRequestEntity.Comments
            };


            if (raiseRequestViewModel == null)
            { 
                return NotFound();
            }

            return View(raiseRequestViewModel);
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
