using FromHeartyAI.DataStructures;
using FromHeartyAI.ML_Model;
using Service.Services;
using Service.Mappers;
using WebAPI.AIPredictEngine;

namespace WebAPI.ApiControllers
{
    static class Endpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost(ServiceDependencyInjection.LOGIN_PATH, (LoginRequest loginRequest, LoginService loginService, MyContext context) =>
                loginService.TryLogin(loginRequest, context)
            );

            app.MapGet(ServiceDependencyInjection.LOGOUT_PATH, (LoginService loginService, HttpContext context) => {
                var token = context.Request.Headers["Authorization"].FirstOrDefault();
                if(token is null)
                    return false;
                return loginService.TryLogout(Guid.Parse(token));
            });

            app.MapGet("/patients", (MyContext context) =>
                context.Patients.Where(x => !x.IsRemoved).ToArray());

            app.MapGet("/patients/{patientId}", (string patientId, MyContext context) =>
            {
                Guid.TryParse(patientId, out Guid guidPatientId);
                var patient = context.Patients
                    .Where(patient => patient.Id == guidPatientId).FirstOrDefault();

                return patient;
            });

            app.MapPut("/patient", (Patient patient, MyContext context) =>
            {
                context.Patients.AddOrUpdate(patient);
                context.SaveChanges();
                return patient;
            });

            app.MapGet("/cardiologists", (MyContext context) =>
                context.Cardiologists.Where(x => !x.IsRemoved).ToArray());

            app.MapPut("/cardiologists", (Cardiologist cardiologist, MyContext context) =>
            {
                context.Cardiologists.AddOrUpdate(cardiologist);
                context.SaveChanges();
                return cardiologist;
            });

            app.MapGet("/house-doctors", (MyContext context) =>
                context.HouseDoctors.Where(x => !x.IsRemoved).ToArray());

            app.MapPut("/house-doctors", (HouseDoctor houseDoctor, MyContext context) =>
            {
                context.HouseDoctors.AddOrUpdate(houseDoctor);
                context.SaveChanges();
                return houseDoctor;
            });

            app.MapGet("/findings", (MyContext context) =>
                context.Findings.Where(x => !x.IsRemoved).ToArray());

            app.MapPut("/finding", (Finding finding, MyContext context) =>
            {
                finding.IsSickPrediction = AiPredictEngine.Predict(finding.FeatureA); // <--- AI

                context.Findings.AddOrUpdate(finding);
                context.SaveChanges();
                return finding;
            });

            app.MapGet("/findings-base", (MyContext context) =>
                context.FindingsBase.Where(x => !x.IsRemoved).ToArray());

            app.MapPut("/finding-base", (FindingBase findingBase, MyContext context) =>
            {
                findingBase.IsSickPrediction = FindingBasePredictEngine.Predict(findingBase);

                context.FindingsBase.AddOrUpdate(findingBase);
                context.SaveChanges();
                return findingBase;
            });

            app.MapPost("/predict", (FromHeartyData predictionRequest, MLModel mlModel) =>
            {
                return (float)Math.Round(mlModel.Predict(predictionRequest).Probability * 100, 2);
            });

            app.MapPost("/open-ai-predict", async (FromHeartyOpenAiData predictionRequest, IPredictionService _predictionService) =>
            {
                return await _predictionService.GetOpenAiDescriptivePrediction(predictionRequest);
            });

            app.MapGet("/appointments", (MyContext context) =>
            context.Appointments.Where(x => !x.IsRemoved).ToArray());

            app.MapPut("/appointments", (Appointment appointment, MyContext context) =>
            {
                context.Appointments.AddOrUpdate(appointment);
                context.SaveChanges();
                return appointment;
            });

            app.MapGet("/appointments/{cardiologistId}", (string cardiologistId, MyContext context) =>
            {
                Guid.TryParse(cardiologistId, out Guid guidCardiologistId);
                var appointments = context.Appointments
                    .Where(appointment => appointment.CardiologistId == guidCardiologistId)
                    .Select(AppointmentMapper.MapAppointmentForCalendar)
                    .ToList();

                return appointments;
            });

            app.MapGet("/appointments/{cardiologistId}/{patientId}", (string cardiologistId, string patientId, MyContext context) =>
            {
                Guid.TryParse(cardiologistId, out Guid guidCardiologistId);
                Guid.TryParse(patientId, out Guid guidPatientId);
                var appointments = context.Appointments
                    .Where(appointment => appointment.CardiologistId == guidCardiologistId && appointment.PatientId == guidPatientId)
                    .ToList();

                return appointments;
            });

            app.MapDelete("/appointments/{appointmentId}", (string appointmentId, MyContext context) =>
            {
                Guid.TryParse(appointmentId, out Guid guidAppointmentId);

                var appointmentsToDelete = context.Appointments
                    .Where(appointment => appointment.Id == guidAppointmentId)
                    .ToList();

                if (appointmentsToDelete.Any())
                {
                    context.Appointments.RemoveRange(appointmentsToDelete);
                    context.SaveChanges();
                }
            });

            app.MapGet("/appointment/{appointmentId}", (string appointmentId, MyContext context) =>
            {
                Guid.TryParse(appointmentId, out Guid guidAppointmentId);
                var appointment = context.Appointments
                    .Where(appointment => appointment.Id == guidAppointmentId).FirstOrDefault();

                return appointment;
            });

            app.MapPut("/appointment", (Appointment appointment, MyContext context) =>
            {
                context.Appointments.Update(appointment);
                context.SaveChanges();
                return appointment;
            });

            app.MapGet("/prediction-result", (MyContext context) =>
            context.PredictionResults.Where(x => !x.IsRemoved).ToArray());

            app.MapPut("/prediction-result", (PredictionResult predictionResult, MyContext context) =>
            {
                context.PredictionResults.AddOrUpdate(predictionResult);
                context.SaveChanges();
                return predictionResult;
            });

            app.MapGet("/prediction-result/{patientId}", (string patientId, MyContext context) =>
            {
                Guid.TryParse(patientId, out Guid guidPatientId);
                var predictionResults = context.PredictionResults
                    .Where(predictionResult => predictionResult.PatientId == guidPatientId)
                    .ToList();

                return predictionResults;
            });

        }
    }
}
