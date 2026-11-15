namespace Panda.Service.Models
{
    public class PatientModel
    {
        public required string NhsNumber { get; init; }
        public required string Name { get; init; }
        public required DateOnly DateOfBirth { get; init; }
        public required string PostCode { get; init; }
    }
}