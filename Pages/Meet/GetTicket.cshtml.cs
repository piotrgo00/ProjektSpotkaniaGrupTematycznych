using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektSpotkaniaGrupTematycznych.Models;
using QRCoder;
using Microsoft.AspNetCore.Hosting;


namespace ProjektSpotkaniaGrupTematycznych.Pages.Meet
{
    public class GetTicketModel : PageModel
    {
        private readonly ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public byte[] GenerateQR(Ticket t)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Ticket.Id.ToString() + "_" + Ticket.Owner.Id + "_" + Ticket.MeetingId.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap QRCode = qrCode.GetGraphic(20);

            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(QRCode, typeof(byte[]));
        }
        public GetTicketModel(ProjektSpotkaniaGrupTematycznych.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Meeting Meeting { get; set; }
        public Group Group { get; set; }
        public Ticket Ticket { get; set; }
        public byte[] QR { get; set; }
        public async Task<IActionResult> OnGetAsync(int? mid)
        {
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

            ApplicationUser CurrentUser = await _userManager.GetUserAsync(User);
            if (_context.Ticket.Where(p => p.MeetingId == Meeting.Id && p.Owner == CurrentUser).Count() > 0)
            {
                Ticket = _context.Ticket.FirstOrDefault(p => p.MeetingId == Meeting.Id && p.Owner == CurrentUser);
                QR = GenerateQR(Ticket);
            }
            else
            {
                Ticket = new Ticket();
                Ticket.GroupName = Group.GroupName;
                Ticket.Topic = Meeting.Topic;
                Ticket.Location = Meeting.Location;
                Ticket.Date = Meeting.Date;
                Ticket.Owner = CurrentUser;
                Ticket.MeetingId = Meeting.Id; 

                _context.Ticket.Add(Ticket);
                _context.SaveChanges();

                Ticket = _context.Ticket.FirstOrDefault(p => p.MeetingId == Meeting.Id && p.Owner == CurrentUser);
                QR = GenerateQR(Ticket);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? mid)
        {
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

            ApplicationUser CurrentUser = await _userManager.GetUserAsync(User);
            Ticket = _context.Ticket.FirstOrDefault(p => p.MeetingId == Meeting.Id && p.Owner == CurrentUser);

            return Page();
        }
    }

}
