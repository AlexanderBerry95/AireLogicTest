using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Business
{
    public record Appointment
    {
        public int Id { get; init; }
        public required string Status { get; init; }
        public required string Text { get; init; }
        public required string Duration { get; init; }
        public required string Clinician { get; init; }
        public required string Department { get; init; }
        public required string PostCode { get; init; }
        public int PatientId { get; init; }
        public Patient Patient { get; init; } = null!;
    }
}
