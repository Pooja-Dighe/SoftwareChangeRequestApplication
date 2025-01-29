using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCRSApplication.Data;
using SCRSApplication.Models;
using System.Linq;

namespace SCRSApplication.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public TeamsController(ApplicationDBContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var teamList = _dbContext.TeamEntity.ToList();

            //var memberList = _dbContext.TeamMemberEntity
            //    .Join(_dbContext.TeamEntity,
            //    tm=> tm.TeamId, 
            //    t => t.Id,
            //    (tm,t)=> new {TM=tm,T=t}
            //    )
            //    .Join(_dbContext.Users,
            //    combined => combined.TM.MemberId,  
            //    u => u.Id ,
            //    (u,combined) =>new TeamsViewModel               
            //    {
            //        TeamMembers = combined.u.UserName,
            //        TeamName = combined.T.TeamName,
            //        Position = TM.Position
            //    }).ToList();



            var memberList = (from tm in _dbContext.TeamMemberEntity
                              join u in _dbContext.Users on tm.MemberId equals u.Id
                              join t in _dbContext.TeamEntity on tm.TeamId equals t.Id
                              select new
                              {
                                  u.UserName,
                                  t.TeamName,
                                  tm.Position
                              }).ToList();
            var viewModel = new TeamsViewModel()
            {
                TeamEntityList = teamList,
                //TeamMemberEntityList = memberList
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var teamList = _dbContext.TeamEntity.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.TeamName
            }).ToList();
           
            var memberList = (from u in _dbContext.Users
                              join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                              join r in _dbContext.Roles on ur.RoleId equals r.Id 
                              where r.Name== "Developer" select new SelectListItem
                              {
                                  Value = u.Id, 
                                  Text=u.UserName
                              }).ToList();
              
            ViewBag.TeamList = teamList;
            ViewBag.MemberList = memberList;
            return View();
        }

        [HttpPost, ActionName("CreateTeam")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeam(TeamsViewModel teamsVM)
        {
            if (!ModelState.IsValid)
            {
                if (teamsVM.Teams != null)
                {

                    TeamsEntity team = new TeamsEntity
                    {
                      Id = teamsVM.Teams.Id,
                      TeamName = teamsVM.Teams.TeamName
                    };
                     _dbContext.TeamEntity.Add(team);
                     await _dbContext.SaveChangesAsync();
                     return RedirectToAction(nameof(Index));
                }
            }
            return View("Index");
        }

        [HttpPost, ActionName("CreateTeamMember")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeamMember(TeamsViewModel teamsVM)
        {
            if (!ModelState.IsValid)
            {
                if (teamsVM.TeamMember != null)
                {
                    TeamMemberEntity team = new TeamMemberEntity
                    {
                        Id = teamsVM.TeamMember.Id,
                        MemberId = teamsVM.TeamMember.MemberId,
                        TeamId=teamsVM.TeamMember.TeamId,
                        Position= teamsVM.TeamMember.Position
                    };
                    _dbContext.TeamMemberEntity.Add(team);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("Index");
        }
    }
}
