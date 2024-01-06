Console.WriteLine("Hello World!");

using var db = new MyContext();

var houseDoctors = new List<HouseDoctor>() {
    new()
    {
        FirstName = "Malaya",
        LastName = "Bradley"
    },
    new()
    {
        FirstName = "Charley",
        LastName = "Rosales"
    },
    new()
    {
        FirstName = "Wilder",
        LastName = "Lynn"
    },
    new()
    {
        FirstName = "Samira",
        LastName = "Peters"
    },
    new()
    {
        FirstName = "Patrick",
        LastName = "Willis"
    },
};


var cardiologists = new List<Cardiologist>() {
    new()
    {
        FirstName = "Alexia",
        LastName = "Weiss"
    },
    new()
    {
        FirstName = "Koa",
        LastName = "Giles"
    },
    new()
    {
        FirstName = "Daniel",
        LastName = "Beasley"
    },
    new()
    {
        FirstName = "Autumn",
        LastName = "Griffith"
    },
    new()
    {
        FirstName = "Franklin",
        LastName = "Acosta"
    },
};

var patients = new List<Patient>() {
    new()
    {
        FirstName = "Kamiyah",
        LastName = "House",
        BirthDate = new DateTime(),
        Gender = true,
        DateRegistered = new DateTime(),
        HouseDoctorId = houseDoctors[0].Id,
    },
    new()
    {
        FirstName = "Abigail",
        LastName = "Atkins",
        BirthDate = new DateTime(),
        Gender = true,
        DateRegistered = new DateTime(),
        HouseDoctorId = houseDoctors[1].Id
    },
    new()
    {
        FirstName = "Lyanna",
        LastName = "Jacobs",
        BirthDate = new DateTime(),
        Gender = true,
        DateRegistered = new DateTime(),
        HouseDoctorId = houseDoctors[2].Id
    },
    new()
    {
        FirstName = "Madisyn",
        LastName = "Solis",
        BirthDate = new DateTime(),
        Gender = true,
        DateRegistered = new DateTime(),
        HouseDoctorId = houseDoctors[3].Id
    },
    new()
    {
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

db.HouseDoctors.AddOrUpdate(houseDoctors);
db.Cardiologists.AddOrUpdate(cardiologists);
db.Patients.AddOrUpdate(patients);
db.Findings.AddOrUpdate(findings);
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
