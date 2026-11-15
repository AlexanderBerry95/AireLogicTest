using Microsoft.EntityFrameworkCore;

namespace Panda.Business
{
    public interface IPandaContext
    {
        DbSet<Patient> Patients { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}