using Panda.Business;
using Panda.Service.Models;

namespace Panda.Service
{
    public interface IPatientService
    {
        Task<Patient> CreatePatient(CreatePatientRequest request);
    }
}