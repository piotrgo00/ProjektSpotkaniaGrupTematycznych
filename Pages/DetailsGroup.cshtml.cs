using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages
{
    public class DetailsGroupModel : PageModel
    {
        private readonly ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext _context;

        public DetailsGroupModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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

            return Page();
        }
    }
}
