using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Data
{
    public class GroupContext : DbContext 
    {
        public GroupContext(DbContextOptions<GroupContext> options) : base(options) { }
        public DbSet<Group> Group { get; set; }
    }
}
