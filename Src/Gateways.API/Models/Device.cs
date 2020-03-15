using System;
using System.ComponentModel;

namespace Gateways.Model {
    public enum DeviceStatus {
        OnLine,
        OffLine
    }

    public class Device {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DeviceStatus Status { get; set; }
        public Guid GatewayId { get; set; }
        public virtual Gateway Gateway { get; set; }
    }
}