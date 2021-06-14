using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        //public string Creator { get; set; } //id
        [Required]
        [MaxLength(20)]
        [Display(Name = "Group Category")]
        public string CategoryName { get; set; }

    }
}
