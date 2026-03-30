using BabyTracker.Data;
using BabyTracker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BabyTrackerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<BabyService>();
builder.Services.AddScoped<TrackerService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BabyTrackerContext>();
    context.Database.EnsureCreated();
    
    // Seed data if database is empty
    if (!context.Babies.Any())
    {
        context.Babies.AddRange(
            new BabyTracker.models.Baby { Name = "Pork", Age = 1 },
            new BabyTracker.models.Baby { Name = "Emma", Age = 6 }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
