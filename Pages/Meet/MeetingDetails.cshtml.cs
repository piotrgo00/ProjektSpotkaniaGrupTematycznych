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
    public class MeetingDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Meeting Meeting { get; set; }
        public Group Group { get; set; }
        public MeetingDetailsModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? mid)
        {
            if (mid == null)
                return NotFound();

            Meeting = _context.Meeting.Include(f => f.Participants).ThenInclude(g => g.User).Where(e => e.Id == mid).FirstOrDefault();
            Group = _context.Group.Where(e => e.Id == Meeting.GroupID).FirstOrDefault();

            return Page();
        }
        public bool IsMeetingAdmin(string userID, Group group)
        {
            if (userID == group.OwnerID)
                return true;
            return false;
        }
        public bool IsInGroup(string userID, int groupID)
        {
            if (_context.UserGroups.Where(p => p.UserId == userID && p.GroupId == groupID).Count() > 0)
                return true;
            return false;
        }
    }
}
