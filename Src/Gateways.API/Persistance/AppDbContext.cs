using Microsoft.EntityFrameworkCore;
using Gateways.Model;
using System;

namespace Gateways.Persistance {
    public class AppDbContext : DbContext {
        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<Device> Devices { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            var g = builder.Entity<Gateway>().ToTable("Gateways");
            g.HasKey(t => t.Id);
            g.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();//.HasValueGenerator<Guid>();
            g.Property(t => t.Name).IsRequired().HasMaxLength(50);
            g.Property(t => t.IP).HasMaxLength(16);
            g.Property(t => t.DeviceNumb).IsConcurrencyToken();
            g.Ignore(t => t.IPAddress);
            g.HasMany(t => t.Devices).WithOne(t => t.Gateway).HasForeignKey(t => t.GatewayId);

            var d = builder.Entity<Device>().ToTable("Devices");
            d.HasKey(t => t.Id);
            d.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            d.Property(t => t.Vendor).IsRequired().HasMaxLength(50);
            d.Property(t => t.CreatedOn).IsRequired();
            d.Property(t => t.Status).IsRequired();
            
            FillTestData(builder);
        }

        private void FillTestData(ModelBuilder builder) {
            var g1 = new Gateway() { Id = Guid.NewGuid(), IPAddress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 1 }), Name = "Gateway 1", DeviceNumb = 3 };
            var g2 = new Gateway() { Id = Guid.NewGuid(), IPAddress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 2 }), Name = "Gateway 2", DeviceNumb = 2 };
            
            builder.Entity<Gateway>().HasData(g1, g2);

            var d11 = new Device() { Id = 101, Vendor = "Cisco", Status = DeviceStatus.OnLine, GatewayId = g1.Id };
            var d12 = new Device() { Id = 102, Vendor = "Dell", Status = DeviceStatus.OffLine, GatewayId = g1.Id };
            var d13 = new Device() { Id = 103, Vendor = "HP", Status = DeviceStatus.OffLine, GatewayId = g1.Id };

            var d21 = new Device() { Id = 201, Vendor = "Huawei", Status = DeviceStatus.OffLine, GatewayId = g2.Id };
            var d22 = new Device() { Id = 202, Vendor = "Fujitsu", Status = DeviceStatus.OnLine, GatewayId = g2.Id };

            builder.Entity<Device>().HasData(d11, d12, d13, d21, d22);
        }
    }
}