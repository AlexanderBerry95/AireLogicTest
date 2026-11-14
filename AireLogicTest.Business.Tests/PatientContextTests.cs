using Microsoft.Data.Sqlite;
using FluentAssertions;
using Microsoft.CodeCoverage.Core.Reports.Coverage;

namespace Panda.Business.Tests
{
    [TestClass]
    public class PatientContextTests
    {
        [TestMethod]
        public void WhenPatientsAddedToDatabase_DatabaseIsNotEmpty()
        {
            using var patientContext = new PatientContext();
            CreateInitialDataset(patientContext);

            Assert.AreNotEqual(0, patientContext.Patients.Count());
        }

        private static void CreateInitialDataset(PatientContext patientContext)
        {
            patientContext.Database.EnsureCreated();
            var patients = new List<Patient>
            {
                new Patient(){ DateOfBirth = new DateOnly(1980, 1, 1), Name = "John Doe", PostCode = "SW1A 1AA", NhsNumber = 1234567890 },
                new Patient(){ DateOfBirth = new DateOnly(1985, 5, 15), Name = "Jane Smith", PostCode = "SW1A 1AA", NhsNumber = 0987654321 }
            };
            patientContext.Patients.AddRange(patients);
            patientContext.SaveChanges();
        }
    }
}
