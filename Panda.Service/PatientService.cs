using Panda.Business;

namespace Panda.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientContext patientContext;

        public PatientService(IPatientContext patientContext)
        {
            this.patientContext = patientContext;
        }

        public void AddPatient(Patient patient)
        {

        }
    }
}
