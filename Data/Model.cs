using Microsoft.EntityFrameworkCore;

public class MyContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Cardiologist> Cardiologists { get; set; }
    public DbSet<HouseDoctor> HouseDoctors { get; set; }
    public DbSet<Finding> Findings { get; set; }
    public DbSet<FindingBase> FindingsBase { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PredictionResult> PredictionResults { get; set; }
    public DbSet<Video> Videos { get; set; }

    public string DbPath { get; }

    public MyContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "from-hearty-database.db");
        Console.WriteLine($"DbPath: {DbPath}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={DbPath}");
}

public interface IModel 
{
    Guid Id { get; set; }
    bool IsRemoved { get; set; }
}

public class UserRole : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public required string Role { get; set; }
    public List<User> User { get; } = [];
}

public class User : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required Guid UserRoleId { get; set; }
    public UserRole UserRole { get; set; } = default!;
    public List<Patient> Patient { get; } = [];
    public List<HouseDoctor> HouseDoctor { get; } = [];
    public List<Cardiologist> Cardiologist { get; } = [];
    public List<Video> Video { get; } = [];
}

public class Patient : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public DateTime DateRegistered { get; set; } = new DateTime();
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required bool? Gender { get; set; }
    public required DateTime BirthDate { get; set; }
    public required Guid HouseDoctorId { get; set; }
    public HouseDoctor HouseDoctor { get; set; } = default!;
    public List<Finding> Findings { get; } = [];
    public List<Appointment> Appointment { get; } = [];
    public List<PredictionResult> PredictionResult { get; } = [];
}

public class Cardiologist : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<Finding> Findings { get; } = [];
    public List<Appointment> Appointment { get; } = [];
}

public class HouseDoctor : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<Patient> Patient { get; } = [];
    public List<Finding> Findings { get; } = [];
    public List<PredictionResult> PredictionResult { get; } = [];
}

public class Finding : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public required DateTime CreatedOn { get; set; } = DateTime.Now;
    public required string FeatureA { get; set; } // <--- Here we need to add features for HearyPro.
    public required bool? IsSickPrediction { get; set; } // <--- AI
    public required bool? IsSick { get; set; } 
    public required Guid PatientId { get; set; }
    public Patient Patient { get; set; } = default!;
    public Guid? CardiologistId { get; set; }
    public Cardiologist? Cardiologist { get; set; }
    public Guid? HouseDoctorId { get; set; }
    public HouseDoctor? HouseDoctor { get; set; }
    public string? HouseDoctorNote { get; set; }
    public string? CardiologistNote { get; set; }
}

public class FindingBase : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public required DateTime CreatedOn { get; set; } = DateTime.Now;
    public required float BMI { get; set; }
    public required bool Smoking { get; set; }
    public required string AlcoholDrinking { get; set; }
    public required bool Stroke { get; set; }
    public required float PhysicalHealth { get; set; }
    public required float MentalHealth { get; set; }
    public required bool DiffWalking { get; set; }
    public required string Sex { get; set; }
    public required string AgeCategory { get; set; }
    public required bool Diabetic { get; set; }
    public required bool PhysicalActivity { get; set; }
    public required string GenHealth { get; set; }
    public required float SleepTime { get; set; }
    public required bool Asthma { get; set; }
    public required bool KidneyDisease { get; set; }
    public required bool SkinCancer { get; set; }
    public required bool? IsSickPrediction { get; set; } // <--- AI
    public required bool? IsSick { get; set; } 
    public required Guid PatientId { get; set; }
    public Patient Patient { get; set; } = default!;
    public Guid? CardiologistId { get; set; }
    public Cardiologist? Cardiologist { get; set; }
    public Guid? HouseDoctorId { get; set; }
    public HouseDoctor? HouseDoctor { get; set; }
    public string? HouseDoctorNote { get; set; }
    public string? CardiologistNote { get; set; }
}

public class Appointment : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public required string Title { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool AllDay { get; set; } = false;
    public string? Description { get; set; }
    public bool Approved { get; set; } = false;
    public DateTime Created { get; set; } = DateTime.Now;
    public Guid CardiologistId { get; set; }
    public Cardiologist Cardiologist { get; set; } = default!;
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = default!;
}

public class PredictionResult : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public float Cp { get; set; }
    public float TrestBps { get; set; }
    public float Chol { get; set; }
    public float Fbs { get; set; }
    public float RestEcg { get; set; }
    public float Thalach { get; set; }
    public float Exang { get; set; }
    public float OldPeak { get; set; }
    public float Slope { get; set; }
    public float Ca { get; set; }
    public float Thal { get; set; }
    public float Label { get; set; }
    public DateTime Created {  get; set; } = DateTime.Now;
    public float Percentage { get; set; }
    public Guid? HouseDoctorId { get; set; }
    public HouseDoctor? HouseDoctor { get; set; }
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = default!;
}


public class Video : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public required string Title { get; set; }
    public required string Link { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}