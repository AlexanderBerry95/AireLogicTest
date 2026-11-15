using Microsoft.EntityFrameworkCore;

namespace Panda.Business
{
    public class PandaContext(DbContextOptions<PandaContext> options) : DbContext(options), IPandaContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .ToTable("Patients", "main")
                .HasKey(patient => patient.Id);

            modelBuilder.Entity<Patient>()
                .Property(patient => patient.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Appointment>()
                .ToTable("Appointments", "main")
                .HasKey(appointment => appointment.Id);

            modelBuilder.Entity<Appointment>()
                .Property(appointment => appointment.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Appointment>()
                .HasOne(appointment => appointment.Patient)
                .WithMany()
                .HasForeignKey(appointment => appointment.PatientId);

            base.OnModelCreating(modelBuilder);
        } 
    }
}