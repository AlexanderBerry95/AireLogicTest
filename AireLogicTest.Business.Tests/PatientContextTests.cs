using FluentAssertions;
using Microsoft.CodeCoverage.Core.Reports.Coverage;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Panda.Business.Tests
{
    [TestClass]
    public class PatientContextTests
    {
        [TestMethod]
        public void WhenPatientsAddedToDatabase_PatientCountMatches()
        {
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            using var patientContext = CreateNewContext(connection);
            patientContext.Database.EnsureCreated();

            var patients = new List<Patient>
            {
                new() { DateOfBirth = new DateOnly(1980, 1, 1), Name = "John Doe", PostCode = "SW1A 1AA", NhsNumber = 1234567890 },
                new() { DateOfBirth = new DateOnly(1985, 5, 15), Name = "Jane Smith", PostCode = "SW1A 1AA", NhsNumber = 987654321 }
            };

            patientContext.Patients.AddRange(patients);
            patientContext.SaveChanges();
            Assert.AreEqual(patients.Count, patientContext.Patients.Count());
        }

        private static PatientContext CreateNewContext(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<PatientContext>()
                .UseSqlite(connection)
                .Options;

            return new PatientContext(options);
        }
    }
}
