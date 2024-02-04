
namespace Service.DTO
{
    public class CalendarApointmentResponse
    {
        public Guid Id { get; set; }
        public bool IsRemoved { get; set; } = false;
        public required string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; } = false;
        public string? Description { get; set; }
        public bool Approved { get; set; } = false;
        public Guid CardiologistId { get; set; }
        public Cardiologist? Cardiologist { get; set; }
        public Guid PatientId { get; set; }
        public Patient? Patient { get; set; }
        public string? BackgroundColor { get; set; }
    }
}
