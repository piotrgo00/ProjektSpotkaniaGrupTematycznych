using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages
{
    [Authorize]
    public class EnlistModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public InvitationRequest InvRequest { get; set; }

        public EnlistModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Group Group { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Group.FirstOrDefaultAsync(m => m.Id == id);

            if (Group == null)
            {
                return NotFound();
            }
            System.Diagnostics.Debug.WriteLine("Podpaski");

            if (Group.OwnerID == _userManager.GetUserId(HttpContext.User))
                return RedirectToPage("./Groups");

            return Page();
        }
        public async Task<IActionResult> OnGetRetryAsync(int? id, bool retrybool)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Group.FirstOrDefaultAsync(m => m.Id == id);

            if (Group == null)
            {
                return NotFound();
            }
            if (Group.OwnerID == _userManager.GetUserId(HttpContext.User))
                return RedirectToPage("./Groups");

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id, bool retrybool)
        {
            System.Diagnostics.Debug.WriteLine(retrybool);
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Group.FindAsync(id);

            if (Group == null)
            {
                //jakis tempdata ze jest error
                return NotFound();
            }
            var userId = _userManager.GetUserId(HttpContext.User);

            if (_context.UserGroups.Where(p => p.GroupId == (int)id && p.UserId == userId).ToList().Count >= 1)
                return Page(); //juz jest w grupie

            if(retrybool == true)
            {
                var _invr = _context.InvitationRequest.Where(p => p.InvokerId == userId && p.GroupID == (int)id && p.Status == InvitationStatus.Declined).ToList(); //istnieje juz taki request
                InvRequest.Status = InvitationStatus.Pending;

            }
            else
            {
                var _inv = _context.InvitationRequest.Where(p => p.InvokerId == userId && p.GroupID == (int)id && p.Status == InvitationStatus.Pending).ToList(); //istnieje juz taki request
                if (_inv.Count > 0)
                {
                    //error message
                    return Page();
                }
            }
            
            InvRequest.GroupID = (int)id;
            InvRequest.RequestDate = DateTime.Now;
            InvRequest.InvokerId = userId;
            InvRequest.Status = InvitationStatus.Pending;

            if (retrybool == true)
            {
                System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(InvRequest));
                System.Diagnostics.Debug.WriteLine("Sperma");
                _context.Attach(InvRequest).State = EntityState.Modified;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Chuj");
                _context.InvitationRequest.Add(InvRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToPage("./Groups");


            //return Page();
        }
    }
}
