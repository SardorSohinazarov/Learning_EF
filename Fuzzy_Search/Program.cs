using System.Text.Json;
using Temporal_Tables;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Hello, World!");

        var _dbContext = new ApplicationDbContext();

        #region adding
        //_dbContext.Posts.AddRange(
        //    new Post()
        //    {
        //        Title = "Sardor",
        //        Body = "This is the first post",
        //        CreatedAt = DateTime.Now
        //    },
        //    new Post()
        //    {
        //        Title = "Sarvar",
        //        Body = "This is the second post",
        //        CreatedAt = DateTime.Now
        //    },
        //    new Post()
        //    {
        //        Title = "Sanjar",
        //        Body = "This is the third post",
        //        CreatedAt = DateTime.Now
        //    }
        //);

        //_dbContext.SaveChanges();
        #endregion

        #region Search
        Console.Write("\nQidiruv so'zi: ");
        var searchTerm = Console.ReadLine();

        var result = _dbContext.Posts
                             .Where(p => ApplicationDbContext.Soundex(p.Title) == ApplicationDbContext.Soundex(searchTerm)) // SOUNDEX qidiruvi
                             .ToList();

        Console.WriteLine($"Natija: {JsonSerializer.Serialize(result)}");

        Main();
        #endregion
    }
}
