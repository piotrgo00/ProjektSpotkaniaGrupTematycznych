using Microsoft.EntityFrameworkCore;
using ProjektSpotkaniaGrupTematycznych.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSpotkaniaGrupTematycznych.Data
{
    public class CategoryContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
    }
}
