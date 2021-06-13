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
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserMeeting> UserMeeting { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserGroup>()
                .HasKey(ug => new { ug.GroupId, ug.UserId });
            builder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(u => u.Members)
                .HasForeignKey(ug => ug.GroupId);
            builder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(g => g.Groups)
                .HasForeignKey(ug => ug.UserId);

            builder.Entity<UserMeeting>()
                .HasKey(ug => new { ug.MeetingId, ug.UserId });
            builder.Entity<UserMeeting>()
                .HasOne(ug => ug.Meeting)
                .WithMany(u => u.Participants)
                .HasForeignKey(ug => ug.MeetingId);
            builder.Entity<UserMeeting>()
                .HasOne(ug => ug.User)
                .WithMany(g => g.Meetings)
                .HasForeignKey(ug => ug.UserId);
            //....
        }
    }
}
