using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages.Meet
{
    [Authorize]
    public class MeetingsModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MeetingsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Meeting> Meetings { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var uid = _userManager.GetUserId(User);
            List<UserGroup> userGroups = _context.UserGroups.Where(e => e.UserId == uid).ToList();
            List<int> userGroupsIds = new List<int>();
            foreach(var item in userGroups)
            {
                userGroupsIds.Add(item.GroupId);
            }
            //group = await _context.Group.Include(g => g.Members).ThenInclude(y => y.User).Include(group => group.Meetings).FirstOrDefaultAsync(m => m.Id == gid);
            Meetings = _context.Meeting.Where(e => userGroupsIds.Contains(e.GroupID)).ToList();

            return Page();
        }
    }
}
