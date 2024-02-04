using Service.DTO;

namespace Service.Mappers
{
    public static class AppointmentMapper
    {
        public static CalendarApointmentResponse MapAppointmentForCalendar(this Appointment appointment)
        {
            return new CalendarApointmentResponse
            {
                Id = appointment.Id,
                IsRemoved = appointment.IsRemoved,
                Title = appointment.Title,
                Start = appointment.Start,
                End = appointment.End,
                AllDay = appointment.AllDay,
                Description = appointment.Description,
                Approved = appointment.Approved,
                CardiologistId = appointment.CardiologistId,
                Cardiologist = appointment.Cardiologist,
                PatientId = appointment.PatientId,
                Patient = appointment.Patient,
                BackgroundColor = appointment.Approved ? "#51A351" : "#F99F1F"
            };
        }
    }
}
