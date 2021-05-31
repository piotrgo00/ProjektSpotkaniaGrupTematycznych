using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Data
{
    public class MeetingContext : DbContext
    {
        public DbSet<Meeting> Meeting { get; set; }
    }
}
