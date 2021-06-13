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

namespace ProjektSpotkaniaGrupTematycznych.Pages
{
    public class MyGroupsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty(SupportsGet = true)]
        public string CategoryName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CityName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string GroupName { get; set; }
        public MyGroupsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Group> Groups { get; set; }
        public IList<Category> Categories { get; set; }
        public async Task OnGetAsync()
        {
            Groups = await _context.Group.Include(m => m.Members).Include(c => c.GroupCategory).ToListAsync();
            // Categories = await _context.Category.ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (CategoryName == null)
                CategoryName = "";
            if (GroupName == null)
                GroupName = "";
            if (CityName == null)
                CityName = "";

            Groups = _context.Group
                    .Where(entity => entity.GroupName.Contains(GroupName) &&
                     entity.GroupCategory.CategoryName.Contains(CategoryName) &&
                     entity.City.Contains(CityName))
                    .OrderByDescending(entity => entity.Id)
                    .Include(m => m.Members).Include(c => c.GroupCategory)
                    .ToList();

            // Categories = await _context.Category.ToListAsync();
            return Page();
        }
    }
}
