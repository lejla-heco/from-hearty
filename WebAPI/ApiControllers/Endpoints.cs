using FromHeartyAI.DataStructures;
using FromHeartyAI.ML_Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.Transforms;
using Service.Services;
using Service.Mappers;
using WebAPI.AIPredictEngine;
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;

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
            {
                var cardiologistsWithUsers = context.Cardiologists
                    .Include(c => c.User)
                    .Where(c => !c.IsRemoved) 
                    .Select(cardiologist => new
                    {
                        cardiologist.Id,
                        Email = cardiologist.User.Email,
                        FirstName = cardiologist.FirstName,
                        LastName = cardiologist.LastName
                    })
                    .ToArray();

                return cardiologistsWithUsers;
            });

            app.MapPut("/cardiologists", (Cardiologist cardiologist, MyContext context) =>
            {
                context.Cardiologists.AddOrUpdate(cardiologist);
                context.SaveChanges();
                return cardiologist;
            });

            app.MapGet("/house-doctors", (MyContext context) =>
            {
                var houseDoctorsWithUsers = context.HouseDoctors
                    .Include(hd => hd.User)
                    .Where(hd => !hd.IsRemoved)
                    .Select(hd => new
                    {
                        hd.Id,
                        Email= hd.User.Email,
                        FirstName = hd.FirstName,
                        LastName = hd.LastName
                    })
                    .ToArray();

                return houseDoctorsWithUsers;
            });

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
                    .Where(appointment => appointment.CardiologistId == guidCardiologistId).AsEnumerable()
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
                var appointment = context.Appointments.FirstOrDefault(appointment => appointment.Id == guidAppointmentId);
                appointment.Start = appointment.Start.ToLocalTime();
                appointment.End = appointment.End.ToLocalTime();
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
                    .OrderByDescending(predictionResult => predictionResult.Created)
                    .ToList();

                return predictionResults;
            });

            app.MapGet("/predictions/house-doctor/{houseDoctorId}", (string houseDoctorId, MyContext context) =>
            {
                Guid.TryParse(houseDoctorId, out Guid guidHouseDoctorId);

                var allMonthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
                .Where(monthName => !string.IsNullOrEmpty(monthName))
                .Select((monthName, index) => new { MonthName = monthName, MonthNumber = index + 1 });

                var predictionsPerMonth = context.PredictionResults
                    .Where(prediction => prediction.HouseDoctorId == guidHouseDoctorId)
                    .AsEnumerable()
                    .GroupBy(prediction => prediction.Created.Month)
                    .ToDictionary(group => group.Key, group => group.Count());

                var data = allMonthNames.Select(month => new { data = new int[] { predictionsPerMonth.ContainsKey(month.MonthNumber) ? predictionsPerMonth[month.MonthNumber] : 0 }, label = month.MonthName });

                return data.ToArray();
            });

            app.MapGet("/appointments/house-doctor/{status}", (string status, MyContext context) =>
            {
                bool approvedStatus = status.ToUpper() == "APPROVED";

                var appointmentsPerMonth = context.Appointments
                    .Where(appointment => appointment.Approved == approvedStatus)
                    .AsEnumerable()
                    .GroupBy(appointment => appointment.Start.Month)
                    .OrderBy(group => group.Key)
                    .ToDictionary(group => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key), group => group.Count());

                var allMonths = CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames;

                var dataArray = allMonths.Select(month => appointmentsPerMonth.ContainsKey(month) ? appointmentsPerMonth[month] : 0).ToArray();

                return dataArray;
            });

            app.MapGet("/approved-appointments/cardiologist/{cardiologistId}", (string cardiologistId, MyContext context) =>
            {
                Guid.TryParse(cardiologistId, out Guid guidCardiologistId);

                var allMonthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
                .Where(monthName => !string.IsNullOrEmpty(monthName))
                .Select((monthName, index) => new { MonthName = monthName, MonthNumber = index + 1 });

                var appointmentsByMonth = context.Appointments
                    .Where(appointment => appointment.CardiologistId == guidCardiologistId && appointment.Approved)
                    .AsEnumerable()
                    .GroupBy(appointment => appointment.Start.Month)
                    .ToDictionary(group => group.Key, group => group.Count());

                var data = allMonthNames.Select(month => new { data = new int[] { appointmentsByMonth.ContainsKey(month.MonthNumber) ? appointmentsByMonth[month.MonthNumber] : 0 }, label = month.MonthName });

                return data.ToArray();

            });

            app.MapGet("/appointments/cardiologist/{cardiologist}", (string cardiologist, MyContext context) =>
            {
                Guid.TryParse(cardiologist, out Guid guidCardiologistId);
                var predictions = context.PredictionResults
                    .Where(predictionResult => predictionResult.PatientId == guidCardiologistId)
                    .ToList();

                return predictions;
            });


            app.MapGet("/prediction-result/percentage/{houseDoctorId}", (string houseDoctorId, MyContext context) =>
            {
                Guid.TryParse(houseDoctorId, out Guid guidHouseDoctorId);
                var predictions = context.PredictionResults
                    .Where(predictionResult => predictionResult.HouseDoctorId == guidHouseDoctorId)
                    .ToList();

                var groupedPredictions = predictions
                    .GroupBy(prediction => prediction.Percentage switch
                    {
                        var p when p >= 0 && p <= 15 => 0,
                        var p when p >= 16 && p <= 55 => 1,
                        _ => 2
                    })
                    .Select(group => new { GroupKey = group.Key, Count = group.Count() })
                    .ToList();

                var result = Enumerable.Range(0, 3)
                    .Select(index => groupedPredictions.FirstOrDefault(x => x.GroupKey == index)?.Count ?? 0)
                    .ToArray();

                return result;
            });

            app.MapGet("/prediction-result/patients", (MyContext context) =>
            {
                var topPredictions = context.PredictionResults
                    .Include(x => x.Patient)
                    .GroupBy(x => x.PatientId)
                    .Select(group => new
                    {
                        PatientName = group.First().Patient.FirstName + " " + group.First().Patient.LastName,
                        MaxPercentage = group.Max(x => x.Percentage)
                    })
                    .OrderByDescending(x => x.MaxPercentage)
                    .Take(10)
                    .ToList();

                var data = topPredictions.Select(prediction => prediction.MaxPercentage).ToList();
                var labels = topPredictions.Select(prediction => prediction.PatientName).ToList();

                return new { data, labels };
            });

            app.MapGet("/videos", (MyContext context) =>
               context.Videos.Where(x => !x.IsRemoved).ToArray());

            app.MapGet("/videos/user/{userId}", (string userId, MyContext context) =>
            {
                Guid.TryParse(userId, out Guid guidUserId);
                var videos = context.Videos
                    .Where(video => video.UserId == guidUserId).ToList();

                return videos;
            });

            app.MapGet("/videos/{videoId}", (string videoId, MyContext context) =>
            {
                Guid.TryParse(videoId, out Guid guidVideoId);
                var video = context.Videos.FirstOrDefault(video => video.Id == guidVideoId);

                return video;
            });

            app.MapPut("/videos", (Video video, MyContext context) =>
            {
                context.Videos.AddOrUpdate(video);
                context.SaveChanges();
                return video;
            });

            app.MapDelete("/videos/{videoId}", (string videoId, MyContext context) =>
            {
                Guid.TryParse(videoId, out Guid guidVideoId);

                var videoToDelete = context.Videos
                    .FirstOrDefault(video => video.Id == guidVideoId);

                if (videoToDelete != null)
                {
                    context.Videos.Remove(videoToDelete);
                    context.SaveChanges();
                }
            });

            #region Documents

            app.MapGet("/user-documents/{userId}", (string userId, MyContext context) =>
            {
                Guid.TryParse(userId, out Guid guidUserId);
                var documents = context.Documents
                    .Where(d => d.UserId == guidUserId)
                    .ToList();

                return documents;
            });

            app.MapPost("/upload-document/{userId}", (string userId, [FromForm] IFormFile file, MyContext context) =>
            {
                Guid.TryParse(userId, out Guid guidUserId);

                var userGuid = guidUserId;

                var extension  = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var fileName = DateTime.Now.Ticks + "." + extension;
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "files");
                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "files",
                    fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                     file.CopyTo(stream);
                }

                var document = new Document
                {
                    Name = file.FileName,
                    Description = "description",
                    CreatedDate = DateTime.Now,
                    FileName = fileName,
                    Extension = extension,
                    UserId = userGuid,
                    IsRemoved = false
                };
                context.Documents.Add(document);
                context.SaveChanges();

            }).DisableAntiforgery();

            app.MapGet("/download-document/{fileId}", (string fileId, MyContext context) =>
            {
                Guid.TryParse(fileId, out Guid guidFileId);

                var file = context.Documents.FirstOrDefault(d => d.Id == guidFileId);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "files", file!.FileName);

                if (!System.IO.File.Exists(filePath))
                    throw new Exception("File not found");

                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;

                return Results.File(memory, GetContentType(filePath), file.Name);

            });

            static string GetContentType(string path)
            {
                var provider = new FileExtensionContentTypeProvider();

                if (!provider.TryGetContentType(path, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                return contentType;
            }

            #endregion

        }
    }
}
