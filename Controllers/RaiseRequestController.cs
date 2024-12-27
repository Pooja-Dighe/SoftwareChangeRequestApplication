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
            var userDetails = await GetUserDetailsAsync();

            ViewBag.UserId = userDetails.UserId;
            ViewBag.UserName = userDetails.UserName;
            ViewBag.RoleId = userDetails.RoleId;
            ViewBag.RoleName = userDetails.RoleName;
            var userId = userDetails.UserId;

            var entities = await _context.RaiseRequestEntity
                          .Where(e => e.UserId == userId)                         
                          .Include(e => e.Project)
                          .ToListAsync();
          
            var PriorityValues = GetPriorityDropdownValues();
       
            var viewModel = entities.Select(e => new RaiseRequestViewModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                DueDate = e.DueDate,             
                Priority = PriorityValues.FirstOrDefault(d => d.Value == e.Priority)?.Text,
                ProjectName = e.Project.ProjectName, // Assuming MyProjectEntity has Name
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var userDetails = await GetUserDetailsAsync();

            ViewBag.UserId = userDetails.UserId;
            ViewBag.UserName = userDetails.UserName;
            ViewBag.RoleId = userDetails.RoleId;
            ViewBag.RoleName = userDetails.RoleName;

            string uid = userDetails.UserId;
            var project = _context.ProjectEntity
                                 .FirstOrDefault(p => _context.Users
                                 .Any(u => u.Id == uid && p.UserId == u.Id));

            ViewBag.ProjectName = project?.ProjectName ?? "No Project Found";
            ViewBag.ProjectId = project?.Id;

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
                    ProjectId = userViewModel.ProjectId,
                    RoleId = userViewModel.RoleId,
                    UserId = userViewModel.UserId,
                    AddedAt = DateTime.Now,
                };

                  _context.RaiseRequestEntity.Add(raiseRequestEntity);
                  _context.SaveChanges();
                  TempData["SuccessMessage"] = "Record saved successfully!";
                  return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while adding record or record is invalid";
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

            string uid = userDetails.UserId;
            var project = _context.ProjectEntity
                                 .FirstOrDefault(p => _context.Users
                                 .Any(u => u.Id == uid && p.UserId == u.Id));

            ViewBag.ProjectName = project?.ProjectName ?? "No Project Found";
            ViewBag.ProjectId = project?.Id;

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
                ProjectId = raiseRequestEntity.ProjectId,
                RoleId = raiseRequestEntity.RoleId,
                UserId = raiseRequestEntity.UserId,               
                RowVersion = raiseRequestEntity.RowVersion
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
                        ProjectId = userViewModel.ProjectId,
                        RoleId = userViewModel.RoleId,
                        UserId = userViewModel.UserId,
                        //RowVersion = userViewModel.RowVersion,
                        UpdatedAt = DateTime.Now
                    };

                    _context.Entry(raiseRequestEntity).OriginalValues["RowVersion"] = userViewModel.RowVersion;
                    _context.RaiseRequestEntity.Update(raiseRequestEntity);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Record updated successfully!";
                    ViewBag.PriorityList = GetPriorityDropdownValues();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateConcurrencyException) 
                {
                    if (!_context.RaiseRequestEntity.Any(p => p.Id == id))
                    {
                        return NotFound(); // Handle cases where the record was deleted
                    }

                    TempData["ErrorMessage"] = "An error occurred while updating the record.";

                    ModelState.AddModelError("", "Concurrency conflict occurred.");
                   
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
            ViewBag.Priorities = GetPriorityDropdownValues();

            var raiseRequestViewModel = new RaiseRequestViewModel
            {
                Id = raiseRequestEntity.Id,
                Title = raiseRequestEntity.Title,
                Description = raiseRequestEntity.Description,
                PriorityValue = raiseRequestEntity.Priority,
                Priority = PriorityValues.FirstOrDefault(d => d.Value == raiseRequestEntity.Priority)?.Text,
                DueDate = raiseRequestEntity.DueDate,
                ProjectId = raiseRequestEntity.ProjectId,
                Comments = raiseRequestEntity.Comments
            };

            if (raiseRequestViewModel == null)
            {
                return NotFound();
            }

            string uid = userDetails.UserId;
            var project = _context.ProjectEntity
                                 .FirstOrDefault(p => _context.Users
                                 .Any(u => u.Id == uid && p.UserId == u.Id));

            raiseRequestViewModel.ProjectName = project?.ProjectName;
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
                ProjectId = raiseRequestEntity.ProjectId,
                Comments = raiseRequestEntity.Comments,
                RowVersion = raiseRequestEntity.RowVersion
            };


            if (raiseRequestViewModel == null)
            { 
                return NotFound();
            }

            string uid = userDetails.UserId;
            var project = _context.ProjectEntity
                                 .FirstOrDefault(p => _context.Users
                                 .Any(u => u.Id == uid && p.UserId == u.Id));

            raiseRequestViewModel.ProjectName = project?.ProjectName;
            return View(raiseRequestViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, byte[] rowVersion)
        {
            try
            {
                var entity = await _context.RaiseRequestEntity
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (entity == null)
                {
                    return NotFound(); // Entity does not exist
                }

                // Set the RowVersion property for concurrency check
                _context.Entry(entity).Property(e => e.RowVersion).OriginalValue = rowVersion;

                _context.RaiseRequestEntity.Remove(entity);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Record deleted successfully!";

                return RedirectToAction(nameof(Index)); // Redirect after successful delete
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception (e.g., if the RowVersion has changed)
                ModelState.AddModelError("", "The record you attempted to delete was modified by another user.");
                TempData["ErrorMessage"] = "Record not found.";
                return View("Index"); // Return to view with an error message
            }
        }
    }
}
