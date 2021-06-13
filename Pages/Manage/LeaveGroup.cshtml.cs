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

namespace ProjektSpotkaniaGrupTematycznych.Pages.Manage
{
    [Authorize]
    public class LeaveGroupModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Group Group { get; set; }
        public LeaveGroupModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? gid)
        {
            if (gid == null)
                return NotFound();

            string uid = _userManager.GetUserId(User);

            Group = await _context.Group.Include(g => g.Members).ThenInclude(y => y.User).Include(group => group.Meetings).FirstOrDefaultAsync(m => m.Id == gid);

            if (Group == null)
                return NotFound();


            if (Group.OwnerID == _userManager.GetUserId(User))
                return Forbid(); //wlasciciel musi usunac grupe, nie moza sam z niej wyjsc

            if (_context.UserGroups.Where(e => e.GroupId == gid && e.UserId == uid).Count() == 0)
                return Forbid(); //nie ma takiego usera w grupie

            return Page();

        }
        public async Task<IActionResult> OnPostAsync(int? gid)
        {
            if (gid == null)
                return NotFound();

            Group = await _context.Group.Include(g => g.Members).ThenInclude(y => y.User).Include(group => group.Meetings).FirstOrDefaultAsync(m => m.Id == gid);

            if (Group == null)
                return NotFound();


            if (Group.OwnerID == _userManager.GetUserId(User))
                return Forbid(); //wlasciciel musi usunac grupe, nie moza sam z niej wyjsc

            

            string uid = _userManager.GetUserId(User);

            if (_context.UserGroups.Where(e => e.GroupId == gid && e.UserId == uid).Count() == 0)
                return Forbid(); //nie ma takiego usera w grupie

            UserGroup RecordToDelete = _context.UserGroups.Where(e => e.UserId == uid).FirstOrDefault();

            Group.Members.Remove(RecordToDelete);

            _context.Attach(Group).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("/DetailsGroup", new { id = Group.Id });
        }
    }
}
