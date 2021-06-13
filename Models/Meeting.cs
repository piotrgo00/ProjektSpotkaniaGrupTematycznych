using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; } //Meeting ID

        [Required]
        [MaxLength(32)]
        public string Topic { get; set; }

        [Required]
        [MaxLength(320)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        [Required]
        [Range(1,999)]
        public int ParticipantLimit { get; set; }

        [Required]
        public int GroupID { get; set; }

        [Display(Name = "Participants")]
        public ICollection<UserMeeting> Participants { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
