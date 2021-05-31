using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string GroupName { get; set; }
        [Required]
        [MaxLength(120)]
        public string GroupDescription { get; set; }
        [Required]
        [MaxLength(20)]
        public string City { get; set; }

        public List<IdentityUser> Members { get; set; }
        public string OwnerID { get; set; }



        public Category GroupCategory { get; set; }

        public List<Meeting> Meetings { get; set; }
    }
}
