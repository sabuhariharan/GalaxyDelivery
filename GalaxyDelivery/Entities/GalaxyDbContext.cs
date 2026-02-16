using GalaxyDelivery.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GalaxyDelivery.Entities
{
    public class GalaxyDbContext : DbContext
    {
        public GalaxyDbContext(DbContextOptions<GalaxyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Driver> Driver { get; set; }

        public DbSet<Vehicle> Vehicle { get; set; }

        public DbSet<DeliveryRoute> Route { get; set; }

        public DbSet<DeliveryEvent> DeliveryEvent { get; set; }

        public DbSet<DeliveryStatus> DeliveryStatus { get; set; }

        public DbSet<Checkpoint> Checkpoint { get; set; }

        public DbSet<Delivery> Delivery { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Driver>().ToTable("Driver");
            builder.Entity<Driver>().HasKey(d => d.DriverId);
            builder.Entity<Driver>().Property(d => d.DriverName).HasMaxLength(100).IsRequired();

            builder.Entity<Vehicle>().ToTable("Vehicle");
            builder.Entity<Vehicle>().HasKey(v => v.VehicleId);
            builder.Entity<Vehicle>().Property(v => v.VehicleMake).HasMaxLength(50).IsRequired();

            builder.Entity<Checkpoint>().ToTable("Checkpoint");
            builder.Entity<Checkpoint>().HasKey(c => c.CheckpointId);
            builder.Entity<Checkpoint>().Property(c => c.CheckpointName).HasMaxLength(50).IsRequired();

            builder.Entity<DeliveryRoute>().ToTable("Route");
            builder.Entity<DeliveryRoute>().HasKey(r => r.RouteId);
            builder.Entity<DeliveryRoute>().Property(r => r.RouteId).ValueGeneratedOnAdd();
            builder.Entity<DeliveryRoute>().Property(r => r.RouteName).HasMaxLength(100).IsRequired();
            builder.Entity<DeliveryRoute>().HasMany(r => r.Checkpoint).WithOne(c => c.Route).HasForeignKey(c => c.RouteId).IsRequired();

            builder.Entity<DeliveryEvent>().ToTable("DeliveryEvent");
            builder.Entity<DeliveryEvent>().HasKey(de => de.DeliveryEventId);
            builder.Entity<DeliveryEvent>().Property(de => de.DeliveryEventDesc).HasMaxLength(250);
            builder.Entity<DeliveryEvent>().Property(de => de.Timestamp).IsRequired();

            builder.Entity<DeliveryStatus>().ToTable("DeliveryStatus");
            builder.Entity<DeliveryStatus>().HasKey(ds => ds.DeliveryStatusId);
            builder.Entity<DeliveryStatus>().Property(ds => ds.StatusName).HasMaxLength(50).IsRequired();

            builder.Entity<DeliveryEvent>().HasOne(de => de.Status).WithMany().HasForeignKey(de => de.StatusId).IsRequired();

            builder.Entity<DeliveryEvent>().HasOne(de => de.Checkpoint).WithMany().HasForeignKey(de => de.CheckpointId);

            builder.Entity<Delivery>().ToTable("Delivery");
            builder.Entity<Delivery>().HasKey(dl => dl.DeliveryId);
            builder.Entity<Delivery>().Property(dl => dl.Origin).HasMaxLength(100).IsRequired();
            builder.Entity<Delivery>().Property(dl => dl.Destination).HasMaxLength(100).IsRequired();
            
            // One-to-One or One-to-Many relationships
            builder.Entity<Delivery>().HasOne(dl => dl.Driver).WithMany().HasForeignKey(dl => dl.DriverId).IsRequired();
            builder.Entity<Delivery>().HasOne(dl => dl.Vehicle).WithMany().HasForeignKey(dl => dl.VehicleId).IsRequired();
            builder.Entity<Delivery>().HasOne(dl => dl.Route).WithMany().HasForeignKey(dl => dl.RouteId).IsRequired();
            
            // One Delivery has Many DeliveryEvents
            builder.Entity<Delivery>().HasMany(dl => dl.DeliveryEvent).WithOne(de => de.Delivery).HasForeignKey(de => de.DeliveryId).IsRequired();
        }
    }
}