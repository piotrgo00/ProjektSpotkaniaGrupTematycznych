using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages.MeetingFolder
{
    public class CreateMeetingModel : PageModel
    {
        private readonly ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext _context;

        public CreateMeetingModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGetAsync(int? gid)
        {

            return Page();
        }

        [BindProperty]
        public Meeting Meeting { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? gid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Meeting.GroupID = (int) gid;
            _context.Meeting.Add(Meeting);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
