using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Context
{
    public class ApplicantDbContext : DbContext
    {
        public ApplicantDbContext(DbContextOptions<ApplicantDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.FamilyName).IsRequired();
                entity.Property(e => e.Address).IsRequired();
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.CountryOfOrigin).IsRequired();
                entity.Property(e => e.EmailAddress).IsRequired();
                entity.Property(e => e.Hired).IsRequired();

                entity.Property(e => e.Hired).HasDefaultValue(false);
                entity.Property(e => e.Active).HasDefaultValue(true);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModifiedOn).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Applicant> Students { get; set; }
    }
}
