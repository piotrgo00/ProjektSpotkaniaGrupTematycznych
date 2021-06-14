using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string QR { get; set; }
        [Required]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Meeting Id")]
        public int MeetingId { get; set; }
    }
}
