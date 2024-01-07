Console.WriteLine("Hello World!");

using var db = new MyContext();

var houseDoctors = new List<HouseDoctor>() {
    new()
    {
        Id = Generator.NewGuid(),
        FirstName = "Malaya",
        LastName = "Bradley"
    },
    new()
    {
        Id = Generator.NewGuid(),
        FirstName = "Charley",
        LastName = "Rosales"
    },
    new()
    {
        Id = Generator.NewGuid(),
        FirstName = "Wilder",
        LastName = "Lynn"
    },
    new()
    {
        Id = Generator.NewGuid(),
        FirstName = "Samira",
        LastName = "Peters"
    },
    new()
    {
        Id = Generator.NewGuid(),
        FirstName = "Patrick",
        LastName = "Willis"
    },
};


var cardiologists = new List<Cardiologist>() {
    new()
    {
        Id = Generator.NewGuid(),
        FirstName = "Alexia",
        LastName = "Weiss"
    },
    new()
    {
        Id = Generator.NewGuid(),
        FirstName = "Koa",
        LastName = "Giles"
    },
    new()
    {
        Id = Generator.NewGuid(),
        FirstName = "Daniel",
        LastName = "Beasley"
    },
    new()
    {
        Id = Generator.NewGuid(),
        FirstName = "Autumn",
        LastName = "Griffith"
    },
    new()
    {
        Id = Generator.NewGuid(),
        FirstName = "Franklin",
        LastName = "Acosta"
    },
};

var patients = new List<Patient>() {
    new()
    {
        Id = Generator.NewGuid(),
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
        FeatureB = "Feature B 1",
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
        FeatureB = "Feature B 2",
        IsSickPrediction = true,
        IsSick = false,
        PatientId = patients[2].Id,
        HouseDoctorNote = "House Doctor Note 2",
        CardiologistNote = "Cardiologist Note 2",
        CardiologistId = cardiologists[2].Id,
        HouseDoctorId = houseDoctors[2].Id
    },
};

db.HouseDoctors.AddOrUpdateRange(houseDoctors);
db.Cardiologists.AddOrUpdateRange(cardiologists);
db.Patients.AddOrUpdateRange(patients);
db.Findings.AddOrUpdateRange(findings);
db.SaveChanges();


// // Read
// var blog = db.Blogs
//     .OrderBy(b => b.Id)
//     .First();

// // Update
// blog.Url = "https://devblogs.microsoft.com/dotnet";
// blog.Posts.Add(new Post
// {
//     Title = "Hello World",
//     Content = "I wrote an app using EF Core!"
// });
// db.SaveChanges();

// // Delete
// db.Remove(blog);
// db.SaveChanges();

// Delete All
// db.RemoveRange(db.HouseDoctors);
// db.RemoveRange(db.Findings);
// db.RemoveRange(db.Cardiologists);
// db.RemoveRange(db.Patients);
// db.SaveChanges();
