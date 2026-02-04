using Microsoft.EntityFrameworkCore;

using NotedApi.Features.Notes;
using NotedApi.Features.Canvases;

namespace NotedApi.Data;

// TODO move this under Persistence sublayer

/// <summary>
/// Database context of Noted app
/// </summary>
public class NotedDbContext : DbContext
{
    public NotedDbContext(DbContextOptions<NotedDbContext> options)
        : base(options)
    { }

    // Add new feature models here
    public DbSet<Canvas> Canvases => Set<Canvas>();
    public DbSet<Note> Notes => Set<Note>();
}
