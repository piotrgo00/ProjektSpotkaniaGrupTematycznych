using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages.Meet
{
    public class MeetingsModel : PageModel
    {
        private readonly ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext _context;

        public MeetingsModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Meeting> Meeting { get;set; }

        public async Task OnGetAsync()
        {
            Meeting = await _context.Meeting.ToListAsync();
        }
    }
}
