using Microsoft.EntityFrameworkCore;
using Panda.Business;
using Panda.Service.Models;

namespace Panda.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientContext patientContext;

        public PatientService(IPatientContext patientContext)
        {
            this.patientContext = patientContext;
        }

        public async Task<Patient> CreatePatient(PatientModel request)
        {
            if(!CheckNhsNumberIsValid(request.NhsNumber.ToString()))
            {
                throw new ArgumentException("Invalid NHS number format.");
            }

            if(!CheckPostCodeIsValid(request.PostCode))
            {
                throw new ArgumentException("Invalid postcode format.");
            }

            var patient = new Patient
            {
                NhsNumber = request.NhsNumber,
                Name = request.Name.Trim(),
                DateOfBirth = request.DateOfBirth,
                PostCode = request.PostCode.Trim()
            };

            return await AddPatientAsync(patient);
        }

        public Task<PatientModel?> GetPatientById(int id)
        {
            return patientContext.Patients
                .Where(p => p.Id == id)
                .Select(p => new PatientModel
                {
                    NhsNumber = p.NhsNumber,
                    Name = p.Name,
                    DateOfBirth = p.DateOfBirth,
                    PostCode = p.PostCode
                })
                .FirstOrDefaultAsync();
        }

        private async Task<Patient> AddPatientAsync(Patient patient)
        {
            bool exists = await patientContext.Patients.AnyAsync(x => x.NhsNumber == patient.NhsNumber);

            if (exists)
            {
                throw new InvalidOperationException("A patient with the specified NHS number already exists.");          
            }

            patientContext.Patients.Add(patient);
            await patientContext.SaveChangesAsync();

            return patient;
        }

        private static bool CheckNhsNumberIsValid(string nhsNumber)
            => System.Text.RegularExpressions.Regex.IsMatch(nhsNumber, @"^\d{3}\s?\d{3}\s?\d{4}$"); // Regex pattern from: https://help.relativity.com/RelativityaiR/Content/Relativity/Relativity_Redact/Regular_expression_examples.htm

        private static bool CheckPostCodeIsValid(string postCode)
            => System.Text.RegularExpressions.Regex.IsMatch(postCode, @"^[A-Z]{1,2}\d[A-Z\d]? \d[A-Z]{2}$", System.Text.RegularExpressions.RegexOptions.IgnoreCase); // Regex pattern from: https://stackoverflow.com/questions/164979/uk-postcode-regex-comprehensive

    }
}
