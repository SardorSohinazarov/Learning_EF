// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Temporal_Tables;

Console.WriteLine("Boshlandi ...");

var _dbContext = new ApplicationDbContext();

#region adding
//_dbContext.Posts.Add(new Post()
//{
//    Title = "First Post",
//    Body = "This is the first post",
//    CreatedAt = DateTime.Now
//});
//_dbContext.SaveChanges();
#endregion

#region updating
//var post = _dbContext.Posts.FirstOrDefault();
//if(post != null)
//{
//    post.Title = "Updated Post 3";
//    post.Body = "This is the updated post 3";
//}
//_dbContext.Update(post);
//_dbContext.SaveChanges();
#endregion

var posts = _dbContext.Posts.TemporalAll();
Console.WriteLine(JsonSerializer.Serialize(posts));

var posts2 = _dbContext.Posts.TemporalFromTo(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
Console.WriteLine(JsonSerializer.Serialize(posts2));

Console.WriteLine("Tugadi ...");