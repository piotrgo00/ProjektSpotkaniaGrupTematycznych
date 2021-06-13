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
    public class LeaveMeetingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public LeaveMeetingModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Meeting Meeting { get; set; }

        public async Task<IActionResult> OnGetAsync(int? mid)
        {
            Meeting = _context.Meeting.Include(f => f.Participants).ThenInclude(g => g.User).Where(e => e.Id == mid).FirstOrDefault();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? mid)
        {
            Meeting = _context.Meeting.Include(f => f.Participants).ThenInclude(g => g.User).Where(e => e.Id == mid).FirstOrDefault();
            string uid = _userManager.GetUserId(User);
            UserMeeting RecordToDelete = _context.UserMeeting.Where(e => e.UserId == uid && e.MeetingId == mid).FirstOrDefault();
            Meeting.Participants.Remove(RecordToDelete);
            _context.Attach(Meeting).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("Meet/MeetingDetails", new { mid = Meeting.Id });
        }
        
    }
}
