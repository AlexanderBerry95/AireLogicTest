using Microsoft.EntityFrameworkCore;

namespace Panda.Business
{
    public interface IPatientContext
    {
        DbSet<Patient> Patients { get; }
    }
}