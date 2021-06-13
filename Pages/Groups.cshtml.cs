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

        public IList<Group> Groups { get;set; }
        public IList<Category> Categories { get; set; }
        public IList<InvitationRequest> InvRequests { get; set; }

        public async Task OnGetAsync()
        {
            Groups = await _context.Group.ToListAsync();
            //System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(Groups));
            Categories = await _context.Category.ToListAsync();
            InvRequests = await _context.InvitationRequest.ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (CategoryName == null)
                CategoryName = "";
            if (GroupName == null)
                GroupName = "";
            if (CityName == null)
                CityName = "";
            Groups = _context.Group.Where(entity => entity.GroupName.Contains(GroupName) && entity.GroupCategory.CategoryName.Contains(CategoryName) && entity.City.Contains(CityName)).OrderByDescending(entity => entity.Id).ToList();
            Categories = await _context.Category.ToListAsync();
            InvRequests = await _context.InvitationRequest.ToListAsync();
            return Page();
        }
    }
}
