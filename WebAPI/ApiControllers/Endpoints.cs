using FromHeartyAI.DataStructures;
using FromHeartyAI.ML_Model;
using Service.Services;
using WebAPI.AIPredictEngine;

namespace WebAPI.ApiControllers
{
    static class Endpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost(ServiceDependencyInjection.LOGIN_PATH, (LoginRequest loginRequest, LoginService loginService) =>
                loginService.TryLogin(loginRequest)
            );

            app.MapGet(ServiceDependencyInjection.LOGOUT_PATH, (LoginService loginService, HttpContext context) => {
                var token = context.Request.Headers["Authorization"].FirstOrDefault();
                if(token is null)
                    return false;
                return loginService.TryLogout(Guid.Parse(token));
            });

            app.MapGet("/patients", (MyContext context) =>
                context.Patients.Where(x => !x.IsRemoved).ToArray());

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

            app.MapPost("/open-ai-predict", async (FromHeartyData predictionRequest, IPredictionService _predictionService) =>
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
                    .ToList();

                return appointments;
            });


        }
    }
}
