using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages.Meet
{
    [Authorize]
    public class DeleteMeetingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteMeetingModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Meeting Meeting { get; set; }
        public Group group { get; set; }
        public async Task<IActionResult> OnGetAsync(int? mid)
        {
            if (mid == null)
                return NotFound();

            Meeting = _context.Meeting.Where(p => p.Id == mid).FirstOrDefault();
            group = _context.Group.Where(p => p.Id == Meeting.GroupID).FirstOrDefault();

            if (Meeting == null)
                return NotFound();
            if (group.OwnerID != _userManager.GetUserId(User))
                return Forbid();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? mid)
        {
            if (mid == null)
                return NotFound();

            Meeting = _context.Meeting.Where(p => p.Id == mid).FirstOrDefault();
            group = _context.Group.Where(p => p.Id == Meeting.GroupID).FirstOrDefault();

            if (Meeting == null)
                return NotFound();
            if (group.OwnerID != _userManager.GetUserId(User))
                return Forbid();

            Meeting ToRemove = await _context.Meeting.FindAsync(mid);

            _context.Meeting.Remove(ToRemove);
            await _context.SaveChangesAsync();
            return RedirectToPage("/DetailsGroup", new { id = group.Id });
        }
    }
}
