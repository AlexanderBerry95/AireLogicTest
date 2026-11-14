using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Panda.Business
{
    public class PatientContext : DbContext
    {
        public DbSet<Patient> Patients;

        public PatientContext()
        {
            Database.EnsureCreated();
            Patients = Set<Patient>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "FileName=Panda", 
                option => 
                { 
                    option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                }
            );
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patients", "main");
            modelBuilder.Entity<Patient>(entity => {
                entity.HasKey(e => e.NhsNumber);
             });
            base.OnModelCreating(modelBuilder);
        }
    }
}
