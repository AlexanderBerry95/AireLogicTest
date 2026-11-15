using Microsoft.EntityFrameworkCore;

namespace Panda.Business
{
    public interface IPandaContext
    {
        DbSet<Patient> Patients { get; }
        DbSet<Appointment> Appointments { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}