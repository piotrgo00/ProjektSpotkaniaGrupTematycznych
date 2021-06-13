using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Data;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages.Manage
{
    public class AcceptRequestModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpRequest _httpRequest;


        public InvitationRequest InvitationRequest { get; set; }
        public AcceptRequestModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();


            InvitationRequest = _context.InvitationRequest.Where(entity => entity.Id == id && entity.Status == InvitationStatus.Pending).FirstOrDefault();
            Group group = _context.Group.Where(entity => entity.Id == InvitationRequest.GroupID).FirstOrDefault();

            if (InvitationRequest == null)
                return NotFound();
            if (group == null)
                return NotFound();
            if (group.OwnerID != _userManager.GetUserId(User))
                return Forbid();

            return Page(); 
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();


            InvitationRequest = _context.InvitationRequest.Where(entity => entity.Id == id && entity.Status == InvitationStatus.Pending).FirstOrDefault();

            if (InvitationRequest == null)
                return NotFound();

            Group group = _context.Group.Where(entity => entity.Id == InvitationRequest.GroupID).FirstOrDefault();

            if (group == null)
                return NotFound();

            if (group.OwnerID != _userManager.GetUserId(User))
                return Forbid();

            InvitationRequest.Status = InvitationStatus.Accepted;
            _context.Entry(group).Collection(b => b.Members).Load();

            ApplicationUser user = _userManager.Users.FirstOrDefault(u => u.Id == InvitationRequest.InvokerId);

            group.Members.Add(new UserGroup { User = user, Group = group, GroupId = group.Id, UserId = user.Id });
            //group.Members.Add(_context.Users.Where(entity => entity.Id == InvitationRequest.InvokerId).FirstOrDefault());

            _context.Attach(InvitationRequest).State = EntityState.Modified;
            _context.Attach(group).State = EntityState.Modified;
            //_context.Attach(group.Members).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            //return Redirect(HttpContext.Request.Headers["Referer"].ToString());
            //return RedirectToPage(HttpContext.Request.);
            return Page(); //need tempdata to return
        }
    }
}
