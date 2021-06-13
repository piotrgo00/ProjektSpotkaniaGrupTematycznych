using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;


namespace ProjektSpotkaniaGrupTematycznych.Pages
{
    public class DetailsGroupModel : PageModel
    {
        private readonly ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsGroupModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Group Group { get; set; }
        public IdentityUser GroupOwner { get; set; }
        public IList<InvitationRequest> InvitationRequests { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Group.Include( g => g.Members).ThenInclude(y => y.User).Include(group => group.Meetings).FirstOrDefaultAsync(m => m.Id == id);
            if (Group == null)
                return NotFound();

            //_context.Entry(Group).Collection(b => b.Members).Load(); //load dependecies
            //Group.Members = await _context.Application
            
            Group.GroupCategory = await _context.Category.FirstOrDefaultAsync(m => m.Id == Group.GroupCategoryId);
            GroupOwner = await _context.Users.FirstOrDefaultAsync(m => m.Id == Group.OwnerID);
            
            InvitationRequests = await _context.InvitationRequest.Where(entity => entity.GroupID == Group.Id).ToListAsync();

            //System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(Group));

            

            return Page();
        }
    }
}
