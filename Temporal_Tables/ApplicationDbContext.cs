using Microsoft.EntityFrameworkCore;
using Temporal_Tables;

public class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .ToTable(buildAction: post => post.IsTemporal());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PostsDatabase;Trusted_Connection=True");
}
