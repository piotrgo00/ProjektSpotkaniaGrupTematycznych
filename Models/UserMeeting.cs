using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class UserMeeting
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
