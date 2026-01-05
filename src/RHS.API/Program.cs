using RHS.API.Utilities;
using RHS.Application;
using RHS.Infrastructure;
using RHS.Persistence;

Console.WriteLine("API starting...");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// åben Package Manager Console
// Add-Migration
// Name: Initial
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations(); // Automatically apply migrations at startup in development
}

// Register exception handling middleware
app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

Console.WriteLine("API started successfully.");
app.Run();

public partial class Program { }
