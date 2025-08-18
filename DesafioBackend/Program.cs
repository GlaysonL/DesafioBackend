using DesafioBackend.Model.Context;
using DesafioBackend.Business;
using DesafioBackend.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using DesafioBackend.Repository.Implementations;
using DesafioBackend.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connection));

builder.Services.AddApiVersioning();

builder.Services.AddScoped<IMotoBusiness, MotoBusinessImplementation>();
builder.Services.AddScoped<IMotoRepository, MotoRepositoryImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
