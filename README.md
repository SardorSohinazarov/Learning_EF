# TPT (Table-per-Type) Mapping Strategy

## 🔹 TPT nima?
**Table-per-Type (TPT)** — bu **inheritance (meros olish)** ni ma’lumotlar bazasida ifodalash strategiyasi.  
Har bir `entity` (`class`) uchun **alohida jadval** yaratiladi.

- **Bazaviy class (`User`)** — umumiy maydonlarni saqlaydi.  
- **Meros oluvchi classlar (`Student`, `Teacher`)** — o‘ziga xos maydonlarni alohida jadvallarda saqlaydi.  

➡️ SQL’da ma’lumot olish uchun **JOIN** ishlatiladi.

---

## 🔹 Misol
```csharp
// User – bazaviy class
// Student va Teacher – undan meros oluvchi classlar

modelBuilder.Entity<User>().UseTptMappingStrategy();
```

Yuqoridagi konfiguratsiya orqali quyidagi jadvallar hosil bo‘ladi:  

- **Users** (bazaviy jadval – umumiy maydonlar)  
- **Students** (faqat studentlarga oid maydonlar)  
- **Teachers** (faqat o‘qituvchilarga oid maydonlar)  

---

## 🔹 Foydalari

### 1. Ma’lumotlarni normalizatsiya qiladi
- Umumiy maydonlar (`FullName`, `PhoneNumber`, `BirthDate`) faqat **Users** jadvalida bo‘ladi.  
- Har bir tur (masalan, `Student`, `Teacher`) o‘zining qo‘shimcha maydonlariga ega bo‘ladi.

### 2. Ortiqcha ustunlardan qutuladi
- Agar **TPH (Table-per-Hierarchy)** ishlatilsa, barcha ustunlar **bitta jadvalda** bo‘ladi.  
  Bu ko‘p `NULL` qiymatlar va chalkashliklarga olib keladi.  
- **TPT esa keraksiz ustunlarsiz toza saqlaydi.**

### 3. O‘qish va tushunish oson
- Jadvallar ko‘rinishi `class` strukturasiga juda yaqin.  
- **DBA** va **dasturchilar** uchun tushunarli bo‘ladi.

### 4. Kengaytirishga qulay
- Yangi tur (`Admin`, `Manager`) qo‘shilsa, faqat **yangi jadval** ochiladi.  
- `Users` jadvali buzilmaydi.

---

## 🔹 Kod namunasi

### Entitylar:
```csharp
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
```

### DbContext:
```csharp
public class ApplicationDb : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPT mapping strategiyasini yoqamiz
        modelBuilder.Entity<User>().UseTptMappingStrategy();

        // Seed ma’lumotlar
        modelBuilder.Entity<Student>().HasData(new Student
        {
            Id = 1,
            FullName = "Sardor Sohinazarov",
            PhoneNumber = "998912040618",
            BirthDate = new DateTime(2003, 2, 20),
            StudentNumber = "S54321",
            Faculty = "Dasturiy injinering",
            EnrollmentDate = new DateTime(2021, 9, 1)
        });

        modelBuilder.Entity<Teacher>().HasData(new Teacher
        {
            Id = 4,
            FullName = "Valiyev Inomjon",
            PhoneNumber = "998903440723",
            BirthDate = new DateTime(1980, 5, 15),
            Subject = "Matematika"
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=learning_ef_mapping_strategies;Integrated Security=True;");
    }
}
```

---

## 🔹 Natija
- **Users** jadvali → umumiy maydonlar  
- **Students** jadvali → faqat studentlarga xos ustunlar  
- **Teachers** jadvali → faqat o‘qituvchilarga xos ustunlar  

So‘rov yuborilganda **JOIN** orqali to‘liq obyekt hosil qilinadi.

---

👉 Bu strategiya **katta loyihalarda**, murakkab `inheritance` bo‘lgan joylarda juda qulay.  
