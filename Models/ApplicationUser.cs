using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Models
{
    public class ApplicationUser : IdentityUser
    {
        //[Display(Name = "Nazwa użytkownika")]
        //public string UserName { get; set; }
        [Display(Name = "Zdjęcie profilowe")]
        public byte[] Image { get; set; }
        //public string AvatarURL { get; set; }
        [Required]
        [MaxLength(25)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(25)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        public ICollection<UserGroup> Groups { get; set; }
    }
}
