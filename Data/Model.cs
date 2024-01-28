using Microsoft.EntityFrameworkCore;

public class MyContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Cardiologist> Cardiologists { get; set; }
    public DbSet<HouseDoctor> HouseDoctors { get; set; }
    public DbSet<Finding> Findings { get; set; }
    public DbSet<FindingBase> FindingsBase { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

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

public class Patient : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    public DateTime DateRegistered { get; set; } = new DateTime();

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required bool? Gender { get; set; }
    public required DateTime BirthDate { get; set; }

    public required Guid HouseDoctorId { get; set; }
    public HouseDoctor HouseDoctor { get; set; } = default!;

    public List<Finding> Findings { get; } = [];
    public List<Appointment> Appointments { get; } = [];
}

public class Cardiologist : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;
    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public List<Finding> Findings { get; } = [];
    public List<Appointment> Appointments { get; } = [];
}

public class HouseDoctor : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; } = false;

    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public List<Patient> Patient { get; } = [];
    public List<Finding> Findings { get; } = [];
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
    public Guid CardiologistId { get; set; }
    public Cardiologist Cardiologist { get; set; } = default!;
    public Guid? PatientId { get; set; }
    public Patient? Patient { get; set; }
}