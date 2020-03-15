using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateways.Mapping {
    public class DeviceWithGatewayResources : DeviceResource {
        public GatewayResource Gateway { get; set; }
    }
}
