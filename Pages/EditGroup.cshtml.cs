using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages
{
    [Authorize]
    public class EditGroupModel : PageModel
    {
        private readonly ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditGroupModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Group Group { get; set; }

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

            //var user = _userManager.GetUserAsync(User);
            var _uid = _userManager.GetUserId(User);
            if (_uid == null || Group.OwnerID != _uid)
                return Forbid();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.Attach(Group).State = EntityState.Modified;
            Group.OwnerID = _userManager.GetUserId(User); // tu sie cos psuje i przy probie zapisu Grupy do bazy, traci ona ownera
            if (Group.GroupCategoryId == null || Group.GroupCategoryId == 0)
            {
                Group.GroupCategory = null;
                Group.GroupCategoryId = null;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(Group.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Groups");
        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.Id == id);
        }
    }
}
