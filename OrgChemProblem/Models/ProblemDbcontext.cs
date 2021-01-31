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

            modelBuilder.Entity<Manager>().HasData(new Manager
            {
                Id = 1,
                UserName = "admin",
                HashPassword = BCrypt.Net.BCrypt.HashPassword("orgchempro")
            });
        }
    }
}