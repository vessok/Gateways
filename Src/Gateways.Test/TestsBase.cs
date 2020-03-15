using AutoMapper;
using Gateways.Controllers;
using Gateways.Mapping;
using Gateways.Model;
using Gateways.Persistance;
using Gateways.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace Gateways.Test {
    public class TestsBase {
        protected DevicesController _deviceController;
        protected GatewaysController _gatewayController;

        [SetUp]
        public void Setup() {

            var config = new MapperConfiguration(opts => {
                opts.AddProfile(new ModelToResourceProfile());
                opts.AddProfile(new ResourceToModelProfile());
            });

            var _mapper = config.CreateMapper(); // Use this mapper to instantiate your class
            
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase("data-in-memory");
            var _dbcontext = new AppDbContext(optionsBuilder.Options);

            var _deviceRepo = new DeviceRepo(_dbcontext);
            var _gatewayRepo = new GatewayRepo(_dbcontext);

            var _gatewayService = new GatewayService(_gatewayRepo);
            var _deviceSerivce = new DeviceService(_deviceRepo, _gatewayRepo);

            _deviceController = new DevicesController(_deviceSerivce, _mapper);
            _gatewayController = new GatewaysController(_gatewayService, _mapper);

            var g1 = new Gateway() { Id = Guid.NewGuid(), IPAddress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 1 }), Name = "Gateway 1" };
            var g2 = new Gateway() { Id = Guid.NewGuid(), IPAddress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 2 }), Name = "Gateway 2" };
            _dbcontext.Gateways.AddRange(g1, g2);

            var d11 = new Device() { Id = 101, Vendor = "Cisco", Status = DeviceStatus.OnLine, GatewayId = g1.Id };
            var d12 = new Device() { Id = 102, Vendor = "Dell", Status = DeviceStatus.OffLine, GatewayId = g1.Id };
            var d13 = new Device() { Id = 103, Vendor = "HP", Status = DeviceStatus.OffLine, GatewayId = g1.Id };

            var d21 = new Device() { Id = 201, Vendor = "Huawei", Status = DeviceStatus.OffLine, GatewayId = g2.Id };
            var d22 = new Device() { Id = 202, Vendor = "Fujitsu", Status = DeviceStatus.OnLine, GatewayId = g2.Id };
            _dbcontext.Devices.AddRange(d11, d12, d13, d21, d22);

            _dbcontext.SaveChanges();
        }
    }
}