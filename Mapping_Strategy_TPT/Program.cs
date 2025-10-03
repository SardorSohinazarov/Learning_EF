//TPT (Table-per-Type) mapping strategiyasi

//🔹 TPT nima?

//Inheritance (meros olish) ni ma’lumotlar bazasida ifodalash strategiyasidir.
//Har bir entity (class) uchun alohida jadval yaratiladi.

//Bazaviy class (User) umumiy maydonlarni saqlaydi, undan meros oluvchi classlar
//(Student, Teacher) esa o‘ziga xos maydonlarni alohida jadvallarida saqlaydi.

//SQL’da JOIN orqali bitta ob’ekt yig‘iladi.


//User – bazaviy class
//Student va Teacher – undan meros oluvchi classlar

//modelBuilder.Entity<User>().UseTptMappingStrategy();
//orqali har bir tur uchun alohida jadval yaratiladi (Users, Students, Teachers).


//🔹 Foydalari

//Ma’lumotlarni normalizatsiya qiladi
//Umumiy maydonlar (FullName, PhoneNumber, BirthDate) faqat bitta jadvalda (Users) saqlanadi.
//Har bir tur (Student, Teacher) o‘zining qo‘shimcha maydonlariga ega bo‘ladi.

//Ortiqcha ustunlardan qutuladi
//Agar TPH (Table-per-Hierarchy) ishlatsangiz, bitta jadvalda barcha ustunlar saqlanadi (Subject, StudentNumber, Faculty ...).
//Bu esa ko‘p null qiymatlar va chalkashliklarga olib keladi.
//TPT esa keraksiz ustunlarsiz toza saqlaydi.

//O‘qish va tushunish oson
//Jadvallar ko‘rinishi class strukturasiga juda yaqin.
//DBA(database admin) va dasturchilar uchun tushunarli.

//Kengaytirishga qulay
//Yangi tur (Admin, Manager) qo‘shsangiz, faqat yangi jadval ochiladi.
//Users jadvalini buzib qo‘ymasdan ishlash mumkin.

#region Using
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var db = new ApplicationDb();
//db.Add(new Student
//{
//    FullName = "John Doe",
//    PhoneNumber = "1234567890",
//    BirthDate = new DateTime(2000, 1, 1),
//    StudentNumber = "S12345",
//    Faculty = "Computer Science",
//    EnrollmentDate = DateTime.Now
//});
//db.SaveChanges();
var options = new JsonSerializerOptions { WriteIndented = true };
Console.WriteLine("Teachers:" + JsonSerializer.Serialize(db.Teachers.ToList(), options));
Console.WriteLine("Students:" + JsonSerializer.Serialize(db.Students.ToList(), options));
Console.WriteLine("Users:" + JsonSerializer.Serialize(db.Users.ToList(), options));
#endregion

#region Db
public class ApplicationDb : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPT mapping strategiyasini yoqamiz
        // https://learn.microsoft.com/en-us/ef/core/modeling/inheritance
        modelBuilder.Entity<User>().UseTptMappingStrategy();

        var students = new List<Student>
        {   new Student
            {
                Id = 1,
                FullName = "Sardor Sohinazarov",
                PhoneNumber = "998912040618",
                BirthDate = new DateTime(2003, 2, 20),
                StudentNumber = "S54321",
                Faculty = "Dasturiy injinering",
                EnrollmentDate = new DateTime(2021, 9, 1)
            },
            new Student
            {
                Id = 2,
                FullName = "Sarvarbek Sohinazarov",
                PhoneNumber = "998912040619",
                BirthDate = new DateTime(2010, 7, 19),
                StudentNumber = "S67890",
                Faculty = "Siyosat",
                EnrollmentDate = new DateTime(2027, 9, 1)
            },
            new Student
            {
                Id = 3,
                FullName = "Sanjarbek Sohinazarov",
                PhoneNumber = "998912040620",
                BirthDate = new DateTime(2002, 11, 5),
                StudentNumber = "S98765",
                Faculty = "Iqtisodiyot",
                EnrollmentDate = new DateTime(2020, 9, 1)
            }
        };

        modelBuilder.Entity<Student>().HasData(students);

        var teachers = new List<Teacher>
        {
            new Teacher
            {
                Id = 4,
                FullName = "Valiyev Inomjon",
                PhoneNumber = "998903440723",
                BirthDate = new DateTime(1980, 5, 15),
                Subject = "Matematika"
            },
            new Teacher
            {
                Id = 5,
                FullName = "Nazirov Shuxratjon",
                PhoneNumber = "998912047322",
                BirthDate = new DateTime(1980, 3, 10),
                Subject = "Fizika"
            }
        };

        modelBuilder.Entity<Teacher>().HasData(teachers);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=learning_ef_mapping_strategies;Integrated Security=True;");
    }
}
#endregion

#region Types
public class User
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
}

public class Student : User
{
    public string StudentNumber { get; set; }
    public string Faculty { get; set; }
    public DateTime EnrollmentDate { get; set; }
}

public class Teacher : User
{
    public string Subject { get; set; }
}
#endregion