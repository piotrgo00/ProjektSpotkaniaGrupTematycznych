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
    public class GroupsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GroupsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Group> Group { get;set; }
        public IList<Category> Category { get; set; }

        public async Task OnGetAsync()
        {
            Group = await _context.Group.ToListAsync();
            Category = await _context.Category.ToListAsync();
        }
    }
}
