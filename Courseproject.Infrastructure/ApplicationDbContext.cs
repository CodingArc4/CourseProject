using Microsoft.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;
using Courseproject.Common.Model;

namespace Courseproject.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Job> Jobs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Address>().HasKey(a => a.Id);
            builder.Entity<Employee>().HasKey(a => a.Id);
            builder.Entity<Team>().HasKey(a => a.Id);
            builder.Entity<Job>().HasKey(a => a.Id);

            builder.Entity<Employee>().HasOne(e => e.Addresses);
            builder.Entity<Employee>().HasOne(e => e.Jobs);

            builder.Entity<Team>().HasMany(e => e.Employees).WithMany(e => e.Teams);

            base.OnModelCreating(builder);
        }
    }
}