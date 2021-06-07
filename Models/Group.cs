﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class Group
    {
        
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Nazwa grupy")]
        public string GroupName { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name = "Opis")]
        public string GroupDescription { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Members")]
        public List<IdentityUser> Members { get; set; }
        
        
        public string OwnerID { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Kategoria")]
        public int? GroupCategoryId { get; set; }

        [Display(Name = "Kategoria")]
        [ForeignKey("GroupCategoryId")]
        public Category GroupCategory { get; set; }

        public List<Meeting> Meetings { get; set; }
    }
}
