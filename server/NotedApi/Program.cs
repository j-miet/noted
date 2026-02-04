using Microsoft.EntityFrameworkCore;

using NotedApi.Infrastructure.Data;
using NotedApi.Infrastructure.Error;
using NotedApi.Features.Canvases;
using NotedApi.Features.Notes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NotedDbContext>(options =>
{
    options.UseSqlite("Data Source=noted.db");
});

// controller interfaces
builder.Services.AddScoped<ICanvasesService, CanvasesService>();
builder.Services.AddScoped<INotesService, NotesService>();

var app = builder.Build();

// if database has no canvases, create default db structure
using (IServiceScope scope = app.Services.CreateScope())
{
    NotedDbContext dataBase = scope.ServiceProvider.GetRequiredService<NotedDbContext>();
    dataBase.Database.EnsureCreated();
    DbSeeder.Seed(dataBase);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandler>();
app.MapControllers();

app.Run();
