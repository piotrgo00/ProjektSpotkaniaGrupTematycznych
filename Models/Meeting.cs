using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Topic { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int ParticipantLimit { get; set; }
    }
}
