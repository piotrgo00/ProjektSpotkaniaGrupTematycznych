using Microsoft.AspNetCore.Identity;
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
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name = "Description")]
        public string GroupDescription { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Members")]
        public ICollection<UserGroup> Members { get; set; }
        
        
        public string OwnerID { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Category")]
        public int? GroupCategoryId { get; set; }

        [Display(Name = "Category")]
        [ForeignKey("GroupCategoryId")]
        public Category GroupCategory { get; set; }

        public ICollection<Meeting> Meetings { get; set; }
    }
}
