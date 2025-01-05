using Microsoft.EntityFrameworkCore;
using Temporal_Tables;

public class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    [DbFunction("SOUNDEX", IsBuiltIn = true)]
    public static string Soundex(string value) => throw new InvalidOperationException();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PostsDatabase_FuzzySearch;Trusted_Connection=True");
}
