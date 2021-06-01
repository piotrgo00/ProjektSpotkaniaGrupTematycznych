using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CreateGroupModel : PageModel
    {
        private readonly ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext _context;
        public IConfiguration _configuration { get; }

        public CreateGroupModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Group Group { get; set; }
        public List<string> Categories 
        { 
            get 
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                //SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=aspnet-ProjektSpotkaniaGrupTematycznych-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true");
                string query = "SELECT CategoryName FROM [dbo].[Category]";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> temporary = new List<string>();
                while (reader.Read())
                {
                    temporary.Add(reader.GetString(0));
                }

                reader.Close();
                con.Close();

                return temporary;
            } 
        }
       // public IList<Category> Categories { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Categories = await _context.Category.ToListAsync();
           // foreach(var x in Categories)
                //cCategories.Add(new SelectListItem() { Text = x.CategoryName, Value = x.Id.ToString() });


            _context.Group.Add(Group);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Groups");
        }
    }
}
