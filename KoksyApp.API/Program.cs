using KoksyApp.API.Logging;
using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using KoksyApp.API.Services;
using KoksyApp.API.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddSingleton( 
    new DatabaseSettings(Environment.GetEnvironmentVariable("DATABASE_URL"), Environment .GetEnvironmentVariable("DATABASE_NAME")));

builder.Services.AddSingleton<IMongoDbClient, MongoDbClient>();
builder.Services.AddScoped<IWorkoutSessionRepository, WorkoutSessionRepository>();
builder.Services.AddScoped<IWorkoutDayRepository, WorkoutDayRepository>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<IWorkoutSessionService, WorkoutSessionService>();
builder.Services.AddScoped<IWorkoutDayService, WorkoutDayService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.Decorate<IWorkoutDayRepository, WorkoutDayRepositoryLogsDecorator>();
builder.Services.Decorate<IWorkoutRepository,WorkoutRepositoryLogs>();
builder.Services.Decorate<IWorkoutSessionRepository, WorkoutSessionRepositoryLogs>();

builder.Logging.AddLog4Net();
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();



app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();