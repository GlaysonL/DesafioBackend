using DesafioBackend.Business;
using DesafioBackend.Business.Implementations;
using DesafioBackend.Messaging;
using DesafioBackend.Model.Context;
using DesafioBackend.Repository;
using DesafioBackend.Repository.Implementations;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseNpgsql(connection));

builder.Services.AddApiVersioning();

builder.Services.AddSwaggerGen(settings =>
{
    settings.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "REST API",
            Version = "v1",
            Description = "Solution to the Rest Api technical challenge",
            Contact = new OpenApiContact
            {
                Name = "Glayson Leonardo",
                Url = new Uri("https://github.com/GlaysonL/"),
            },
        }
    );
});

builder.Services.AddScoped<IMotorcycleBusiness, MotorcycleBusinessImplementation>();
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepositoryImplementation>();
builder.Services.AddScoped<IDeliveryDriverBusiness, DeliveryDriverBusinessImplementation>();
builder.Services.AddScoped<IDeliveryDriverRepository, DeliveryDriverRepositoryImplementation>();
builder.Services.AddScoped<IRentalBusiness, RentalBusinessImplementation>();
builder.Services.AddScoped<IRentalRepository, RentalRepositoryImplementation>();

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() =>
{
    Task.Run(() =>
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<
                IDbContextFactory<AppDbContext>
            >();
            var consumer = new MotorcycleRegisteredConsumer(dbContext);
            consumer.Start();
            Console.WriteLine("RabbitMQ consumer iniciado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Falha ao iniciar o consumidor RabbitMQ: {ex.Message}");
        }
    });
});

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(settings =>
{
    settings.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "Solution to the Rest Api technical challenge - v1"
    );
});

var option = new RewriteOptions().AddRedirect("^$", "swagger");

app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();

app.Run();
