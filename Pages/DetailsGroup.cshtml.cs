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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Group.FirstOrDefaultAsync(m => m.Id == id);
            Group.GroupCategory = await _context.Category.FirstOrDefaultAsync(m => m.Id == Group.GroupCategoryId);
            GroupOwner = await _context.Users.FirstOrDefaultAsync(m => m.Id == Group.OwnerID);

            System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(Group));

            if (Group == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
