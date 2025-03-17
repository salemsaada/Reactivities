using Microsoft.EntityFrameworkCore;
using Persistence;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.WithOrigins("https://localhost:3000","http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
app.MapControllers();
using var scope  = app.Services.CreateScope();
var services =  scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<Logger<Program>>();
    logger.LogError(ex, "an error accured during migration");
    
}
app.Run();
