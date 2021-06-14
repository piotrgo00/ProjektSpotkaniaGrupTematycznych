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
    public class EditMeetingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditMeetingModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Meeting Meeting { get; set; }

        public async Task<IActionResult> OnGetAsync(int? mid)
        {
            if (mid == null)
                return NotFound();

            Meeting = _context.Meeting.Include(p=>p.Participants).ThenInclude(d=>d.User).Where(c=>c.Id == mid).FirstOrDefault();
            var uid = _userManager.GetUserId(User);
            if (_context.Group.Find(Meeting.GroupID).OwnerID != uid)
                return Forbid();



            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? mid)
        {

            if (mid == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Meeting.Id = (int)mid;
            _context.Attach(Meeting).State = EntityState.Modified;

            //Meeting = _context.Meeting.Include(p => p.Participants).ThenInclude(d => d.User).Where(c => c.Id == mid).FirstOrDefault();
            //var uid = _userManager.GetUserId(User);


            await _context.SaveChangesAsync();
            return RedirectToPage("/Meet/MeetingDetails", new { mid = Meeting.Id });
        }
    }
}
