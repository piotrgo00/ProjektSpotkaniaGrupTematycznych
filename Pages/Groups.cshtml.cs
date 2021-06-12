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

        [BindProperty(SupportsGet = true)]
        public string CategoryName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CityName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string GroupName { get; set; }
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (CategoryName == null)
                CategoryName = "";
            if (GroupName == null)
                GroupName = "";
            if (CityName == null)
                CityName = "";
            Group = _context.Group.Where(entity => entity.GroupName.Contains(GroupName) && entity.GroupCategory.CategoryName.Contains(CategoryName) && entity.City.Contains(CityName)).ToList();
            Category = await _context.Category.ToListAsync();
            return Page();
        }
    }
}
