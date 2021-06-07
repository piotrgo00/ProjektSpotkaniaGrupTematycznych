using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class InvitationRequest
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string InvokerId { get; set; }

        
        public string Reason { get; set; }


        public int GroupID { get; set; }
    }
}
