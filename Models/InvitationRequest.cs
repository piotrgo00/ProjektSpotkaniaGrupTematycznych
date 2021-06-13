using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class InvitationRequest
    {
        public InvitationRequest() { }
        public InvitationRequest(DateTime RequestDate, string Invoker, string Reason, int GroupID)
        {
            this.RequestDate = RequestDate;
            this.InvokerId = Invoker;
            this.Reason = Reason;
            this.GroupID = GroupID;
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "Data")]
        public DateTime RequestDate { get; set; }
        [Display(Name = "Sender")]
        public string InvokerId { get; set; }

        [Display(Name = "Reason")]
        [MaxLength(255)]
        public string Reason { get; set; }


        public int GroupID { get; set; }
        public bool Pending { get; set; }
        public bool Declined { get; set; }
    }
}
