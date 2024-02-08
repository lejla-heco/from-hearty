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
                Title = "Appointment 1",
                Start = DateTime.Now,
                End = DateTime.Now,
                PatientId = patients[0].Id,
                CardiologistId = cardiologists[0].Id,
            },
            new () {
                Id = Generator.NewGuid(),
                Title = "Appointment 2",
                Start = DateTime.Now.AddDays(1),
                End = DateTime.Now.AddDays(1),
                PatientId = patients[1].Id,
                CardiologistId = cardiologists[0].Id,
            },
             new () {
                Id = Generator.NewGuid(),
                Title = "Appointment 3",
                Start = DateTime.Now.AddDays(2),
                End = DateTime.Now.AddDays(2),
                PatientId = patients[2].Id,
                CardiologistId = cardiologists[1].Id,
            }
        };

        context.UserRoles.AddOrUpdateRange(userRoles);
        context.Users.AddOrUpdateRange(users);
        context.HouseDoctors.AddOrUpdateRange(houseDoctors);
        context.Cardiologists.AddOrUpdateRange(cardiologists);
        context.Patients.AddOrUpdateRange(patients);
        context.Findings.AddOrUpdateRange(findings);
        context.Appointments.AddOrUpdateRange(appointments);
        context.SaveChanges();
    }
}
