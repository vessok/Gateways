using System;
using System.Collections.Generic;
using System.Net;

namespace Gateways.Model {
    public class Gateway {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] IP { get; set; }
        public short DeviceNumb { get; set; }
        public virtual IList<Device> Devices { get; set; } = new List<Device>();
        public IPAddress IPAddress {
            get { return new IPAddress(IP); }
            set { IP = value.GetAddressBytes(); }
        }
    }
}