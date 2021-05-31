using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class Meeting
    {
        public int Id { get; set; }
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
    }
}
