using DesafioBackend.Model.Context;
using DesafioBackend.Business;
using DesafioBackend.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using DesafioBackend.Repository.Implementations;
using DesafioBackend.Repository;
using Serilog;

using EvolveDb;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connection));

builder.Services.AddApiVersioning();

builder.Services.AddSwaggerGen(settings => {
    settings.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Desafio Backend API",
            Version = "v1",
            Description = "API para o Desafio Backend da empresa ****U"
        });
});

builder.Services.AddScoped<IMotorcycleBusiness, MotorcycleBusinessImplementation>();
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepositoryImplementation>();
builder.Services.AddScoped<IDeliveryDriverBusiness, DeliveryDriverBusinessImplementation>();
builder.Services.AddScoped<IDeliveryDriverRepository, DeliveryDriverRepositoryImplementation>();
builder.Services.AddScoped<IRentalBusiness, RentalBusinessImplementation>();
builder.Services.AddScoped<IRentalRepository, RentalRepositoryImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();