using GalaxyDelivery.Entities;
using GalaxyDelivery.Events;
using GalaxyDelivery.Events.Handlers;
using GalaxyDelivery.Middleware;
using GalaxyDelivery.Services;
using GalaxyDelivery.Services.Interfaces;
using GalaxyDelivery.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GalaxyDelivery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddValidatorsFromAssembly(typeof(DriverValidator).Assembly);
            builder.Services.AddDbContext<GalaxyDbContext>(opt => opt.UseInMemoryDatabase("GalaxyDelivery"));
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IRouteService, RoleService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<ICheckpointService, CheckpointService>();
            builder.Services.AddScoped<IDeliveryService, DeliveryService>();
            builder.Services.AddScoped<IDeliveryEventService, DeliveryEventService>();

            builder.Services.AddScoped<IDomainEventPublisher, InMemoryDomainEventPublisher>();
            builder.Services.AddScoped<IDomainEventHandler<DeliveryFinalizedEvent>, DeliveryFinalizedLoggingHandler>();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Seed initial data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DataGenerator.Initialize(services);
            }

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}