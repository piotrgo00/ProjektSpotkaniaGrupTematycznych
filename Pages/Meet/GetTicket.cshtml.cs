using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektSpotkaniaGrupTematycznych.Models;

namespace ProjektSpotkaniaGrupTematycznych.Pages.Meet
{
    public class GetTicketModel : PageModel
    {
        private readonly ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext _context;
        public GetTicketModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Meeting Meeting { get; set; }
        public Group Group { get; set; }
        public async Task<IActionResult> OnGetAsync(int? mid)
        {
                //if (_context.UserMeeting.Where(p => p.MeetingId == meeting.Id && p.UserId == userID).Count() > 0)
            if (mid != null)
            {
                Meeting = _context.Meeting.FirstOrDefault(x => x.Id == mid);
                if (Meeting != null)
                {
                    Group = _context.Group.FirstOrDefault(x => x.Id == Meeting.GroupID);
                    if (Group == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
                return NotFound();

            //if(_context.Ticket.Where(p => p.MeetingId == Meeting.Id && p.))

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? mid)
        {
            Meeting = _context.Meeting.FirstOrDefault(x => x.Id == mid);
            Group = _context.Group.FirstOrDefault(x => x.Id == Meeting.GroupID);
            Ticket Ticket = new Ticket();

            Ticket.QR = "x";
            Ticket.GroupName = Group.GroupName;
            Ticket.Topic = Meeting.Topic;
            Ticket.Location = Meeting.Location;
            Ticket.Date = Meeting.Date;
            //Ticket.
            //_context.Ticket.Add(Ticket);

            return Page();
        }
    }
}
