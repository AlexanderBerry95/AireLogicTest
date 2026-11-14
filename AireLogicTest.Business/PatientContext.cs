using Microsoft.EntityFrameworkCore;

namespace Panda.Business
{
    public class PatientContext(DbContextOptions<PatientContext> options) : DbContext(options), IPatientContext
    {
        public DbSet<Patient> Patients => Set<Patient>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .ToTable("Patients", "main")
                .HasKey(patient => patient.NhsNumber);

            base.OnModelCreating(modelBuilder);
        }
    }
}