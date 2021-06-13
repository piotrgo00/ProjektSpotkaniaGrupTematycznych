using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages.Meet
{
    public class JoinMeetingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Meeting Meeting { get; set; }
        public Group Group { get; set; }

        public JoinMeetingModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? mid)
        {
            if (mid == null)
                return NotFound();
            Meeting = _context.Meeting.Include(f => f.Participants).ThenInclude(g => g.User).Where(e => e.Id == mid).FirstOrDefault();
            //Group = _context.Group.Where(e => e.Id == Meeting.GroupID).FirstOrDefault();

            

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? mid)
        {
            if (mid == null)
                return NotFound();
            Meeting = _context.Meeting.Include(f => f.Participants).ThenInclude(g => g.User).Where(e => e.Id == mid).FirstOrDefault();
            //Group = _context.Group.Where(e => e.Id == Meeting.GroupID).FirstOrDefault();

            var uid = _userManager.GetUserId(User);
            ApplicationUser user = _userManager.Users.FirstOrDefault(u => u.Id == uid);

            if (Meeting.Participants.Count >= Meeting.ParticipantLimit)
                RedirectToPage("Meet/LimitReached");

            Meeting.Participants.Add(new UserMeeting { Meeting = this.Meeting, MeetingId = this.Meeting.Id, User = user, UserId = user.Id });

            _context.Attach(Meeting).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return RedirectToPage("Meet/DetailsGroup", new { mid = Meeting.Id }); //need tempdata to return
        }
    }
}
