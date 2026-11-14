namespace Panda.Service.Models
{
    public class CreatePatientRequest
    {
        public required int NhsNumber { get; init; }
        public required string Name { get; init; }
        public required DateOnly DateOfBirth { get; init; }
        public required string PostCode { get; init; }
    }
}