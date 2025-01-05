using System.Text.Json;
using Temporal_Tables;

internal class Program
{
    private static void Main()
    {
        var _dbContext = new ApplicationDbContext();

        #region adding
        //_dbContext.Posts.AddRange(
        //    new Post()
        //    {
        //        Title = "Sardor",
        //        Body = "Soxinazarov",
        //        CreatedAt = DateTime.Now
        //    },
        //    new Post()
        //    {
        //        Title = "Sarvar",
        //        Body = "Sohinazarov",
        //        CreatedAt = DateTime.Now
        //    },
        //    new Post()
        //    {
        //        Title = "Sanjar",
        //        Body = "Sohibnazarov",
        //        CreatedAt = DateTime.Now
        //    }
        //);

        //_dbContext.SaveChanges();
        #endregion

        #region Search
        Console.Write("\nQidiruv so'zi: ");
        var searchTerm = Console.ReadLine();

        var result = _dbContext.Posts
                             .Where(p => ApplicationDbContext.Soundex(p.Title) == ApplicationDbContext.Soundex(searchTerm)
                                      || ApplicationDbContext.Soundex(p.Body) == ApplicationDbContext.Soundex(searchTerm)) // SOUNDEX qidiruvi
                             .ToList();

        Console.WriteLine($"Natija: {JsonSerializer.Serialize(result, options: new JsonSerializerOptions() { WriteIndented = true })}");

        Main();
        #endregion
    }
}
