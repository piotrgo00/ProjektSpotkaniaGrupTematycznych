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

namespace ProjektSpotkaniaGrupTematycznych.Pages.Manage
{
    public class DeleteMemberModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Group group { get; set; }
        public async Task<IActionResult> OnGetAsync(string? uid, int? gid)
        {
            if (uid == null || gid == null)
                return NotFound();
            return Page();

            group = await _context.Group.Include(g => g.Members).ThenInclude(y => y.User).Include(group => group.Meetings).FirstOrDefaultAsync(m => m.Id == gid);
            if (group == null)
                return NotFound();

            if (group.OwnerID != _userManager.GetUserId(User))
                return Forbid();

        }
        public async Task<IActionResult> OnPostAsync(string? uid, int? gid)
        {
            if (uid == null || gid == null)
                return NotFound();

            group = await _context.Group.Include(g => g.Members).ThenInclude(y => y.User).Include(group => group.Meetings).FirstOrDefaultAsync(m => m.Id == gid);
            if (group == null)
                return NotFound();

            if (group.OwnerID != _userManager.GetUserId(User))
                return Forbid();


            UserGroup RecordToDelete = _context.UserGroups.Where(e => e.UserId == uid).FirstOrDefault();

            group.Members.Remove(RecordToDelete);

            _context.Attach(group).State = EntityState.Modified;
            //_context.Attach(UserGroup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Page();
        }
    }
}
