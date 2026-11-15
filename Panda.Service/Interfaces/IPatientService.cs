using Panda.Business;
using Panda.Service.Models;

namespace Panda.Service.Interfaces
{
    public interface IPatientService
    {
        Task<int> CreatePatient(PatientModel request);
        Task DeletePatient(int id);
        Task<PatientModel?> GetPatientModelById(int id);
        Task UpdatePatient(PatientModel updateModel);
    }
}