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
    public class DeleteParticipantModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteParticipantModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public ApplicationUser _user { get; set; }
        public UserMeeting userMeeting { get; set; }
        public async Task<IActionResult> OnGetAsync(string? uid, int? mid)
        {
            if (uid == null || mid == null)
                return NotFound();

            _user = _context.Users.Where(p => p.Id == uid).FirstOrDefault();
            if (_user == null)
                return NotFound();

            userMeeting = _context.UserMeeting.Include(p => p.User).Include(m => m.Meeting).Where(d => d.MeetingId == mid && d.UserId == uid).FirstOrDefault();
            if (userMeeting == null)
                return NotFound();
            // = await _context.Group.Include(g => g.Members).ThenInclude(y => y.User).Include(group => group.Meetings).FirstOrDefaultAsync(m => m.Id == gid);

            if (_context.Group.Where(g => g.Id == userMeeting.Meeting.GroupID).FirstOrDefault().OwnerID != _userManager.GetUserId(User))
                return Forbid();

           

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string? uid, int? mid)
        {
            if (uid == null || mid == null)
                return NotFound();


            userMeeting = _context.UserMeeting.Include(p => p.User).Include(m => m.Meeting).Where(d => d.MeetingId == mid && d.UserId == uid).FirstOrDefault();
            if (userMeeting == null)
                return NotFound();
            // = await _context.Group.Include(g => g.Members).ThenInclude(y => y.User).Include(group => group.Meetings).FirstOrDefaultAsync(m => m.Id == gid);

            if (_context.Group.Where(g => g.Id == userMeeting.Meeting.GroupID).FirstOrDefault().OwnerID != _userManager.GetUserId(User))
                return Forbid();

            _context.UserMeeting.Remove(userMeeting);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Meet/MeetingDetails", new {mid = userMeeting.MeetingId });
        }

    }
}
