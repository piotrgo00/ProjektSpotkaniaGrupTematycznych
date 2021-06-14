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
using Microsoft.AspNetCore.Authorization;

namespace ProjektSpotkaniaGrupTematycznych.Pages
{
    public class GroupsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        [BindProperty(SupportsGet = true)]
        public string CategoryName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CityName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string GroupName { get; set; }
        public GroupsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Group> Group { get;set; }
        public IList<Category> Category { get; set; }
        public IList<InvitationRequest>? InvRequests { get; set; }
        public ApplicationUser CurrentUser { get; set; }

        public async Task OnGetAsync()
        {
            Group = await _context.Group.Include(m => m.Members).ToListAsync();
            CurrentUser = await _userManager.GetUserAsync(User);
            if (CurrentUser == null)
            {
                CurrentUser = new ApplicationUser();
                CurrentUser.Id = "0";
            }
            InvRequests = await _context.InvitationRequest.Where(x => x.InvokerId == CurrentUser.Id).ToListAsync();
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

            Group = _context.Group.Where(entity => entity.GroupName.Contains(GroupName) && entity.GroupCategory.CategoryName.Contains(CategoryName) && entity.City.Contains(CityName)).OrderByDescending(entity => entity.Id).ToList();
            Category = await _context.Category.ToListAsync();

            return Page();
        }
    }
}
