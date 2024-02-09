using Microsoft.EntityFrameworkCore;

public static class ModelExtensions
{
    public static void AddOrUpdateRange<T>(this DbSet<T> dbSet, List<T> data) where T : class, IModel =>
        data.ForEach(d =>
        {
            var entity = dbSet.Find(d.Id);
            if (entity == null)
                dbSet.Add(d);
        });

    public static void AddOrUpdate<T>(this DbSet<T> dbSet, T data) where T : class, IModel
    {
        var entity = dbSet.AsNoTracking().FirstOrDefault(x => x.Id == data.Id);
        if (entity == null)
            dbSet.Add(data);
        else
            dbSet.Update(data);
    }

    public static void SetUpData(this MyContext context)
    {
        var userRoles = new List<UserRole>()
        {
            new()
            {
                Id = Generator.NewGuid(),
                Role = "Doctor"
            },
            new()
            {
                Id = Generator.NewGuid(),
                Role = "Cardiolog"
            },
            new()
            {
                Id = Generator.NewGuid(),
                Role = "Patient"
            },
        };

        var users = new List<User>()
        {
            new()
            {
                Id = Generator.NewGuid(),
                Email = "malaya.bradley@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[0].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "charley.rosales@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[0].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "wilder.lynn@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[0].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "samira.peters@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[0].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "patrick.willis@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[0].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "alexia.weiss@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[1].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "koa.giles@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[1].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "daniel.beasley@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[1].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "autumn.griffith@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[1].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "franklin.acosta@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[1].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "kamiyah.house@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[2].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "abigail.atkins@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[2].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "lyanna.jacobs@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[2].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "madisyn.solis@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[2].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                Email = "maliyah.poole@edu.fit.ba",
                Password = "IC2024",
                UserRoleId = userRoles[2].Id,
            }
        };

        var houseDoctors = new List<HouseDoctor>() {
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[0].Id,
                FirstName = "Malaya",
                LastName = "Bradley"
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[1].Id,
                FirstName = "Charley",
                LastName = "Rosales"
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[2].Id,
                FirstName = "Wilder",
                LastName = "Lynn"
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[3].Id,
                FirstName = "Samira",
                LastName = "Peters"
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[4].Id,
                FirstName = "Patrick",
                LastName = "Willis"
            },
        };


        var cardiologists = new List<Cardiologist>() {
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[5].Id,
                FirstName = "Alexia",
                LastName = "Weiss"
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[6].Id,
                FirstName = "Koa",
                LastName = "Giles"
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[7].Id,
                FirstName = "Daniel",
                LastName = "Beasley"
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[8].Id,
                FirstName = "Autumn",
                LastName = "Griffith"
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[9].Id,
                FirstName = "Franklin",
                LastName = "Acosta"
            },
        };

        var patients = new List<Patient>() {
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[10].Id,
                FirstName = "Kamiyah",
                LastName = "House",
                BirthDate = new DateTime(),
                Gender = true,
                DateRegistered = new DateTime(),
                HouseDoctorId = houseDoctors[0].Id,
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[11].Id,
                FirstName = "Abigail",
                LastName = "Atkins",
                BirthDate = new DateTime(),
                Gender = true,
                DateRegistered = new DateTime(),
                HouseDoctorId = houseDoctors[1].Id
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[12].Id,
                FirstName = "Lyanna",
                LastName = "Jacobs",
                BirthDate = new DateTime(),
                Gender = true,
                DateRegistered = new DateTime(),
                HouseDoctorId = houseDoctors[2].Id
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[13].Id,
                FirstName = "Madisyn",
                LastName = "Solis",
                BirthDate = new DateTime(),
                Gender = true,
                DateRegistered = new DateTime(),
                HouseDoctorId = houseDoctors[3].Id
            },
            new()
            {
                Id = Generator.NewGuid(),
                UserId = users[14].Id,
                FirstName = "Maliyah",
                LastName = "Poole",
                BirthDate = new DateTime(),
                Gender = true,
                DateRegistered = new DateTime(),
                HouseDoctorId = houseDoctors[4].Id
            },
        };

        var findings = new List<Finding>() {
            new () {
                Id = Generator.NewGuid(),
                CreatedOn = new DateTime(),
                FeatureA = "Feature A 1",
                IsSickPrediction = true,
                IsSick = true,
                PatientId = patients[0].Id,
                HouseDoctorNote = "House Doctor Note 1",
                CardiologistNote = "Cardiologist Note 1",
                CardiologistId = cardiologists[0].Id,
                HouseDoctorId = houseDoctors[0].Id
            },
            new () {
                Id = Generator.NewGuid(),
                CreatedOn = new DateTime(),
                FeatureA = "Feature A 2",
                IsSickPrediction = true,
                IsSick = false,
                PatientId = patients[2].Id,
                HouseDoctorNote = "House Doctor Note 2",
                CardiologistNote = "Cardiologist Note 2",
                CardiologistId = cardiologists[2].Id,
                HouseDoctorId = houseDoctors[2].Id
            },
        };

        var appointments = new List<Appointment>() {
            new () {
                Id = Generator.NewGuid(),
                Title = "Kamiyah House",
                Start = new DateTime(2024,02,09,08,00,00,00),
                End = new DateTime(2024,02,09,09,30,00,00),
                PatientId = patients[0].Id,
                CardiologistId = cardiologists[0].Id,
                Approved = true,
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Abigail Atkins",
                Start = new DateTime(2024,02,10,08,00,00,00),
                End = new DateTime(2024,02,10,09,30,00,00),
                PatientId = patients[1].Id,
                CardiologistId = cardiologists[0].Id,
            },
             new () {
                Id = Generator.NewGuid(),
                Title = "Kamiyah House",
                Start = new DateTime(2024,02,12,08,00,00,00),
                End = new DateTime(2024,02,12,09,30,00,00),
                PatientId = patients[0].Id,
                CardiologistId = cardiologists[0].Id,
            },
             new () {
                Id = Generator.NewGuid(),
                Title = "Lyanna Jacobs",
                Start = new DateTime(2024,02,05,15,00,00,00),
                End = new DateTime(2024,02,05,17,30,00,00),
                PatientId = patients[2].Id,
                CardiologistId = cardiologists[0].Id,
                Approved = true,
            },
             new () {
                Id = Generator.NewGuid(),
                Title = "Lyanna Jacobs 2",
                Start = new DateTime(2024,02,15,14,00,00,00),
                End = new DateTime(2024,02,15,15,30,00,00),
                PatientId = patients[2].Id,
                CardiologistId = cardiologists[0].Id,
                Approved = true,
            },
             new () {
                Id = Generator.NewGuid(),
                Title = "Kamiyah House",
                Start = new DateTime(2024,01,09,08,00,00,00),
                End = new DateTime(2024,01,09,09,30,00,00),
                PatientId = patients[0].Id,
                CardiologistId = cardiologists[0].Id,
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Abigail Atkins",
                Start = new DateTime(2024,01,10,08,00,00,00),
                End = new DateTime(2024,01,10,09,30,00,00),
                PatientId = patients[1].Id,
                CardiologistId = cardiologists[0].Id,
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Abigail Atkins",
                Start = new DateTime(2024,01,12,08,00,00,00),
                End = new DateTime(2024,01,12,09,30,00,00),
                PatientId = patients[1].Id,
                CardiologistId = cardiologists[0].Id,
                Approved = true,
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Abigail Atkins",
                Start = new DateTime(2024,01,01,08,00,00,00),
                End = new DateTime(2024,01,01,09,30,00,00),
                PatientId = patients[1].Id,
                CardiologistId = cardiologists[0].Id,
                Approved= true,
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Abigail Atkins",
                Start = new DateTime(2024,01,03,08,00,00,00),
                End = new DateTime(2024,01,03,09,30,00,00),
                PatientId = patients[1].Id,
                CardiologistId = cardiologists[0].Id,
                Approved = true,
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Kamiyah House",
                Start = new DateTime(2024,01,20,08,00,00,00),
                End = new DateTime(2024,01,20,09,30,00,00),
                PatientId = patients[0].Id,
                CardiologistId = cardiologists[0].Id,
                Approved = true,
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Kamiyah House",
                Start = new DateTime(2024,01,17,08,00,00,00),
                End = new DateTime(2024,01,17,09,30,00,00),
                PatientId = patients[0].Id,
                CardiologistId = cardiologists[0].Id,
                Approved = true,
            },
             new () {
                Id = Generator.NewGuid(),
                Title = "Kamiyah House",
                Start = new DateTime(2024,03,12,08,00,00,00),
                End = new DateTime(2024,03,12,09,30,00,00),
                PatientId = patients[0].Id,
                CardiologistId = cardiologists[0].Id,
                Approved = true
            },
             new () {
                Id = Generator.NewGuid(),
                Title = "Lyanna Jacobs",
                Start = new DateTime(2024,05,05,15,00,00,00),
                End = new DateTime(2024,05,05,17,30,00,00),
                PatientId = patients[2].Id,
                CardiologistId = cardiologists[0].Id,
                Approved = true,
            },
             new () {
                Id = Generator.NewGuid(),
                Title = "Lyanna Jacobs 2",
                Start = new DateTime(2024,10,15,14,00,00,00),
                End = new DateTime(2024,10,15,15,30,00,00),
                PatientId = patients[2].Id,
                CardiologistId = cardiologists[0].Id,
                Approved = true,
            }
        };

        var videos = new List<Video>() {
            new () {
                Id = Generator.NewGuid(),
                Title = "Symptoms of a heart attack",
                Link = "https://www.youtube.com/watch?v=1i58QD5aWRo&ab_channel=AmericanHeartAssociation",
                UserId = users[0].Id
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Women vs. Men Heart Attack Symptoms",
                Link = "https://www.youtube.com/watch?v=io4Ovh-Q2IA&ab_channel=AmericanHeartAssociation",
                UserId = users[0].Id
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Heart Attack symptoms",
                Link = "https://www.youtube.com/watch?v=1i58QD5aWRo&ab_channel=AmericanHeartAssociation",
                UserId = users[5].Id
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "What is Hands-Only CPR?",
                Link = "https://www.youtube.com/watch?v=_IP--TGgKFI&ab_channel=AmericanHeartAssociation",
                UserId = users[5].Id
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Cardiovascular System 1, Heart, Structure and Function",
                Link = "https://www.youtube.com/watch?v=VWamhZ8vTL4&ab_channel=Dr.JohnCampbell",
                UserId = users[5].Id
            },
        };

        var predictionResults = new List<PredictionResult>() {
            new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,02,09),
                PatientId = patients[0].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 125,
                Chol = 212,
                Fbs = 0,
                RestEcg = 1,
                Thalach = 168,
                Exang = 0,
                OldPeak = 1,
                Slope = 2,
                Ca = 2,
                Thal = 3,
                Label = 0,
                Percentage = 25.15F
            },
            new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,02,10),
                PatientId = patients[1].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 140,
                Chol = 203,
                Fbs = 1,
                RestEcg = 0,
                Thalach = 155,
                Exang = 1,
                OldPeak = 3,
                Slope = 0,
                Ca = 0,
                Thal = 3,
                Label = 0,
                Percentage = 33.33F
            },
             new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,02,12),
                PatientId = patients[0].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 100,
                Chol = 248,
                Fbs = 0,
                RestEcg = 0,
                Thalach = 122,
                Exang = 0,
                OldPeak = 1,
                Slope = 1,
                Ca = 0,
                Thal = 2,
                Label = 1,
                Percentage = 98.99F
            },
             new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,02,05),
                PatientId = patients[2].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 118,
                Chol = 210,
                Fbs = 0,
                RestEcg = 1,
                Thalach = 192,
                Exang = 0,
                OldPeak = 0,
                Slope = 2,
                Ca = 0,
                Thal = 2,
                Label = 1,
                Percentage = 85.17F
            },
             new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,02,15),
                PatientId = patients[3].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 118,
                Chol = 210,
                Fbs = 0,
                RestEcg = 1,
                Thalach = 192,
                Exang = 0,
                OldPeak = 0,
                Slope = 2,
                Ca = 0,
                Thal = 2,
                Label = 1,
                Percentage = 85.17F
            },
             new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024, 01, 09),
                PatientId = patients[4].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 140,
                Chol = 211,
                Fbs = 1,
                RestEcg = 0,
                Thalach = 165,
                Exang = 0,
                OldPeak = 0,
                Slope = 2,
                Ca = 0,
                Thal = 2,
                Label = 1,
                Percentage = 95.25F
            },
            new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024, 01, 10), 
                PatientId = patients[1].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 140,
                Chol = 203,
                Fbs = 1,
                RestEcg = 0,
                Thalach = 155,
                Exang = 1,
                OldPeak = 3,
                Slope = 0,
                Ca = 0,
                Thal = 3,
                Label = 0,
                Percentage = 35.77F
            },
            new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,01,12),
                PatientId = patients[1].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 106,
                Chol = 223,
                Fbs = 0,
                RestEcg = 1,
                Thalach = 142,
                Exang = 0,
                OldPeak = 0,
                Slope = 2,
                Ca = 2,
                Thal = 2,
                Label = 1,
                Percentage = 75.68F
            },
            new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,01,01),
                PatientId = patients[4].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 130,
                Chol = 131,
                Fbs = 0,
                RestEcg = 1,
                Thalach = 115,
                Exang = 1,
                OldPeak = 1,
                Slope = 1,
                Ca = 1,
                Thal = 3,
                Label = 0,
                Percentage = 21.27F
            },
            new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,01,03),
                PatientId = patients[1].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 140,
                Chol = 203,
                Fbs = 1,
                RestEcg = 0,
                Thalach = 155,
                Exang = 1,
                OldPeak = 3,
                Slope = 0,
                Ca = 0,
                Thal = 3,
                Label = 0,
                Percentage = 34.99F
            },
            new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,01,20),
                PatientId = patients[0].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 138,
                Chol = 175,
                Fbs = 0,
                RestEcg = 1,
                Thalach = 173,
                Exang = 0,
                OldPeak = 0,
                Slope = 2,
                Ca = 4,
                Thal = 2,
                Label = 1,
                Percentage = 10.55F
            },
            new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,01,17),
                PatientId = patients[0].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 140,
                Chol = 203,
                Fbs = 1,
                RestEcg = 0,
                Thalach = 155,
                Exang = 1,
                OldPeak = 3,
                Slope = 0,
                Ca = 0,
                Thal = 3,
                Label = 0,
                Percentage = 35.55F
            },
             new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,03,12),
                PatientId = patients[0].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 128,
                Chol = 208,
                Fbs = 1,
                RestEcg = 0,
                Thalach = 140,
                Exang = 0,
                OldPeak = 0,
                Slope = 2,
                Ca = 0,
                Thal = 2,
                Label = 1,
                Percentage = 78.44F
            },
             new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,05,05),
                PatientId = patients[2].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 110,
                Chol = 206,
                Fbs = 0,
                RestEcg = 0,
                Thalach = 108,
                Exang = 1,
                OldPeak = 0,
                Slope = 1,
                Ca = 1,
                Thal = 2,
                Label = 0,
                Percentage = 12.87F
            },
             new () {
                Id = Generator.NewGuid(),
                Created = new DateTime(2024,10,15),
                PatientId = patients[2].Id,
                HouseDoctorId = houseDoctors[0].Id,
                TrestBps = 140,
                Chol = 203,
                Fbs = 1,
                RestEcg = 0,
                Thalach = 155,
                Exang = 1,
                OldPeak = 3,
                Slope = 0,
                Ca = 0,
                Thal = 3,
                Label = 0,
                Percentage = 35.14F
            }
        };


        context.UserRoles.AddOrUpdateRange(userRoles);
        context.Users.AddOrUpdateRange(users);
        context.HouseDoctors.AddOrUpdateRange(houseDoctors);
        context.Cardiologists.AddOrUpdateRange(cardiologists);
        context.Patients.AddOrUpdateRange(patients);
        context.Findings.AddOrUpdateRange(findings);
        context.Appointments.AddOrUpdateRange(appointments);
        context.Videos.AddOrUpdateRange(videos);
        context.PredictionResults.AddOrUpdateRange(predictionResults);
        context.SaveChanges();
    }
}
