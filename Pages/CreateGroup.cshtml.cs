using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages
{
    [Authorize]
    public class CreateGroupModel : PageModel
    {
        private readonly ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CreateGroupModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Group Group { get; set; }
        [BindProperty]
        public List<Category> Categories
        {
            get
            {
                var categories = _context.Category.ToList();
                List<Category> temp = new List<Category>();
                temp.Add(new Category());
                foreach (var x in categories)
                {
                    temp.Add(x);
                }
                return temp;
            }
            set { }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {    
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(ModelState));
                return Page();
            }

            if (_context.Group.Where(m => m.OwnerID == _userManager.GetUserId(User)).Count() >= 3) //tutaj ma sie odwolywac do appsettings
            {
                return Page(); // i tutaj jaks wiadomosc
            }

                
            if (Group.GroupCategoryId == null || Group.GroupCategoryId == 0)
            {
                Group.GroupCategory = null;
                Group.GroupCategoryId = null;
            }
            else
            {
                Group.GroupCategory = await _context.Category.FirstOrDefaultAsync(x => x.Id == Group.GroupCategoryId);
                /*foreach (var x in Categories)
                {
                    if (Group.GroupCategoryId == x.Id)
                        Group.GroupCategory = x;
                }*/
            }
            
            Group.OwnerID = _userManager.GetUserId(HttpContext.User);


            _context.Group.Add(Group);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Groups");
        }
    }
}
