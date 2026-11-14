using Panda.Business;
using Panda.Service.Models;

namespace Panda.Service
{
    public interface IPatientService
    {
        Task<Patient> CreatePatient(PatientModel request);
        Task DeletePatient(int id);
        Task<PatientModel?> GetPatientModelById(int id);
    }
}