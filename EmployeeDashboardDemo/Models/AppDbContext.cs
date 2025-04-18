using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeDashboardDemo.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<Division> Divisions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PositionDescription> PositionDescriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Disable cascade delete for Division -> PositionDescriptions
            modelBuilder.Entity<PositionDescription>()
                .HasRequired(p => p.Division)
                .WithMany(d => d.PositionDescriptions)
                .HasForeignKey(p => p.DivisionId)
                .WillCascadeOnDelete(false);

            // Disable cascade delete for Department -> PositionDescriptions
            modelBuilder.Entity<PositionDescription>()
                .HasRequired(p => p.Department)
                .WithMany(d => d.PositionDescriptions)
                .HasForeignKey(p => p.DepartmentId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}