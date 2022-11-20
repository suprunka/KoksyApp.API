using System;
using System.Text;
using KoksyApp.API.Logging;
using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using KoksyApp.API.Services;
using KoksyApp.API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var x= Environment.GetEnvironmentVariables();
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
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserDayRepository, UserDayRepository>();
builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        policy  =>
        {
            policy.WithHeaders("*");
            policy.WithMethods("*");
            policy.WithOrigins("*");
        });
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.RequireHttpsMetadata = false;
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT").ToString())),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Logging.AddLog4Net();
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyAllowSpecificOrigins");



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();