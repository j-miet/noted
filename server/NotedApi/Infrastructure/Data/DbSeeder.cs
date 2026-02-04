using NotedApi.Features.Canvases;
using NotedApi.Features.Notes;

namespace NotedApi.Data;

/// <summary>
/// Inserts default data into database
/// </summary>
public static class DbSeeder
{
    /// <summary>
    /// Inserts default seed data into database if there's not a single canvas
    /// </summary>
    /// <param name="db">NotedDbContext database context object</param>
    public static void Seed(NotedDbContext db)
    {
        if (db.Canvases.Any())
            return;

        Canvas canvas = new()
        {
            Name = "My Canvas",
            Notes =
            {
                new Note {},
                new Note { Text = "this is a note", X = 400, Y = 500 },
                new Note { Text = "this note uses default X and Y positions"}
            }
        };

        db.Canvases.Add(canvas);
        db.SaveChanges();
    }
}
