using DesafioBackend.Model.Context;
using DesafioBackend.Business;
using DesafioBackend.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using DesafioBackend.Repository.Implementations;
using DesafioBackend.Repository;
using Serilog;

using EvolveDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connection));

//if(builder.Environment.IsDevelopment())
//{
//    MigrateDatabase(connection);
//}

builder.Services.AddApiVersioning();

builder.Services.AddScoped<IMotoBusiness, MotoBusinessImplementation>();
builder.Services.AddScoped<IMotoRepository, MotoRepositoryImplementation>();
builder.Services.AddScoped<IEntregadorBusiness, EntregadorBusinessImplementation>();
builder.Services.AddScoped<IEntregadorRepository, EntregadorRepositoryImplementation>();
builder.Services.AddScoped<ILocacaoBusiness, LocacaoBusinessImplementation>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepositoryImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//void MigrateDatabase(string connection)
//{
//    try
//    {
//        var evolveConnection = new Npgsql.NpgsqlConnection(connection);
//        var evolve = new Evolve(evolveConnection, Log.Information)
//        {
//            Locations = new List<string> { "db/migrations", "db/dataset" },
//            IsEraseDisabled = true,
//            CommandTimeout = 60
//        };
//        evolve.Migrate();
//    }
//    catch (Exception ex)
//    {
//       Log.Error("Database migration failed:", ex);
//        return;
//    }
//}