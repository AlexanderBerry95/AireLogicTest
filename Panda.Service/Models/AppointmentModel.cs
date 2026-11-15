namespace Panda.Service.Models
{
    public class AppointmentModel
    {
        public int Id { get; init; }
        public required string Status { get; init; }
        public required string Text { get; init; }
        public required string Duration { get; init; }
        public required string Clinician { get; init; }
        public required string Department { get; init; }
        public required string PostCode { get; init; }
        public required int PatientId { get; init; }
    }
}
