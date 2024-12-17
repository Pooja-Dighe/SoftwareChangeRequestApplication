using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCRSApplication.Data;
using SCRSApplication.Models;

namespace SCRSApplication.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ProjectController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ProjectController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            //var getall = (from project in _context.ProjectEntity
            //              join user in _context.Users on project.UserId equals user.Id
            //              select new { 
            //                  project.ProjectName, 
            //                  user.UserName
            //              }).ToList();
     
            var applicationDBContext = _context.ProjectEntity.
                Include(p => p.User)
                .Select(p => new ProjectViewModel
                {
                    ProjectName = p.ProjectName,
                    User = p.User.UserName
                } ).ToList();
            return View(applicationDBContext.ToList());
      
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectEntity = await _context.ProjectEntity
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectEntity == null)
            {
                return NotFound();
            }

            return View(projectEntity);
        }

        // GET: Project/Create
        public async Task<IActionResult> Create()
        {
            var userSelectList = await GetUsersSelectListAsync(_context);
            ViewData["UserId"] = userSelectList; // Pass to Viewdata or ViewModel
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectViewModel projectVM)
        {
            if (ModelState.IsValid)
            {
                ProjectEntity projectEntity = new ProjectEntity
                {
                    ProjectName = projectVM.ProjectName,
                    UserId = projectVM.UserId
                };
                _context.Add(projectEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var userSelectList = await GetUsersSelectListAsync(_context);
            ViewData["UserId"] = userSelectList; // Pass to ViewBag or ViewModel
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", projectEntity.UserId);
            return View(projectVM);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectEntity = await _context.ProjectEntity.FindAsync(id);
            if (projectEntity == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", projectEntity.UserId);
            return View(projectEntity);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectName,UserId")] ProjectEntity projectEntity)
        {
            if (id != projectEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectEntityExists(projectEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", projectEntity.UserId);
            return View(projectEntity);
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectEntity = await _context.ProjectEntity
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectEntity == null)
            {
                return NotFound();
            }

            return View(projectEntity);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectEntity = await _context.ProjectEntity.FindAsync(id);
            if (projectEntity != null)
            {
                _context.ProjectEntity.Remove(projectEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectEntityExists(int id)
        {
            return _context.ProjectEntity.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<SelectListItem>> GetUsersSelectListAsync(ApplicationDBContext context)
        {
            var getuser = await (from user in context.Users
                                 join userRole in context.UserRoles on user.Id equals userRole.UserId
                                 join role in context.Roles on userRole.RoleId equals role.Id
                                 where role.Name == "User"
                                 select user).ToListAsync();

            return getuser.Select(user => new SelectListItem
            {
                Text = user.UserName, // Display value
                Value = user.Id       // Underlying value
            });

           
        }
    }
}
