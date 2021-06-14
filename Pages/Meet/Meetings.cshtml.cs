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

            //naprawic wlasicieli grup
            var uid = _userManager.GetUserId(User);
            //List<UserGroup> userGroups = _context.UserGroups.Where(e => e.UserId == uid).ToList();
            List<Group> ownedGroups = _context.Group.Where(e => e.OwnerID == uid).ToList();
            List<UserMeeting> userMeetings = _context.UserMeeting.Where(e => e.UserId == uid).ToList();
            List<int> ownedGroupsIds = new List<int>();
            foreach (var item in ownedGroups)
            {
                ownedGroupsIds.Add(item.Id);
            }
            

            List<int> MeetingsIds = new List<int>();

            foreach (var item in userMeetings)
            {
                MeetingsIds.Add(item.MeetingId);
            }

            List<Meeting> OwnedMeetings = _context.Meeting.Where(m => ownedGroupsIds.Contains(m.GroupID)).ToList();
            foreach(var item in OwnedMeetings)
            {
                MeetingsIds.Add(item.Id);
            }

            //group = await _context.Group.Include(g => g.Members).ThenInclude(y => y.User).Include(group => group.Meetings).FirstOrDefaultAsync(m => m.Id == gid);
            Meetings = _context.Meeting.Where(e => MeetingsIds.Contains(e.Id)).ToList();


            return Page();
        }
        public bool IsInMeeting(Meeting meeting, string userID)
        {
            if (_context.UserMeeting.Where(p => p.MeetingId == meeting.Id && p.UserId == userID).Count() > 0)
                return true;
            return false;
        }
    }
}
