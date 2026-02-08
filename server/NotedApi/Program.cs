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
builder.Services.AddCors(options =>
{
    options.AddPolicy("NotedClient", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173").AllowAnyHeader().AllowAnyMethod();
        // .AllowCredentials(); --add this if client credentials are needed in the future
    });
});

// controller interfaces
builder.Services.AddScoped<ICanvasesService, CanvasesService>();
builder.Services.AddScoped<INotesService, NotesService>();

var app = builder.Build();

// ---Remove this after frontend can send and receive data---
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
app.UseCors("NotedClient");
app.UseAuthorization();
app.UseMiddleware<ExceptionHandler>();
app.MapControllers();

app.Run();
