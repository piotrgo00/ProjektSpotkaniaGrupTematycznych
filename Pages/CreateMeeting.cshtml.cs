using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages
{
    public class CreateMeetingModel : PageModel
    {
        private readonly ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext _context;

        public CreateMeetingModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public int _id { get; set; }
        public IActionResult OnGetAsync(int id)
        {
            _id = id;
            return Page();
        }

        [BindProperty]
        public Meeting Meeting { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Meeting.GroupID = _id;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Meeting.Add(Meeting);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
