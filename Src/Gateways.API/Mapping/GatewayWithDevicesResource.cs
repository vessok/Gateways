using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateways.Mapping {
    public class GatewayWithDevicesResource : GatewayResource {
        public List<DeviceResource> Devices { get; set; }
    }
}
