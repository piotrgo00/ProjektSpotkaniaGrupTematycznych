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
    public class AcceptRequestModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AcceptRequestModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Group Group { get; set; }
        public InvitationRequest InvRequest { get; set; }
        public ApplicationUser Invoker { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InvRequest = _context.InvitationRequest.FirstOrDefault(m => m.Id == id);
            Group = _context.Group.FirstOrDefault(g => g.Id == InvRequest.GroupID);
            Invoker = await _userManager.FindByIdAsync(InvRequest.InvokerId);
            
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InvRequest = _context.InvitationRequest.FirstOrDefault(m => m.Id == id);
            Group = _context.Group.FirstOrDefault(g => g.Id == InvRequest.GroupID);
            Invoker = await _userManager.FindByIdAsync(InvRequest.InvokerId);

            InvRequest.Pending = false;
           // Group.Members.Add(Invoker);

            return RedirectToPage("/Groups");
        }
    }
}
