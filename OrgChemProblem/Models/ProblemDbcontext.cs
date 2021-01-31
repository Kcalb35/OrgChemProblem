using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OrgChemProblem.Models
{
    public class ProblemDbcontext : DbContext
    {
        public ProblemDbcontext(DbContextOptions<ProblemDbcontext> opt) : base(opt)
        {
        }

        public DbSet<Problem> Problems { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Problem>().Property(e => e.Tags).HasConversion(
                v => string.Join(" ", v),
                v => v.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList()
            );

            // admin orgchempro
            modelBuilder.Entity<Manager>().HasData(new Manager
            {
                Id = 1,
                UserName = "admin",
                HashPassword = "fb2d45603825126005378c83049ca239f47e4b6b3e3d97acc259f18d5225781e"
            });
        }
    }
}