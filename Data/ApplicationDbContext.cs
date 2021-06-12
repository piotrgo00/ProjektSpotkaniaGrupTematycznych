using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektSpotkaniaGrupTematycznych.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Meeting> Meeting { get; set; }

        public DbSet<InvitationRequest> InvitationRequest { get; set; }
        
    }
}
