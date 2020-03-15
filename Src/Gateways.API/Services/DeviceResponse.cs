using Gateways.Model;

namespace Gateways.Services {
    public class DeviceResponse : BaseResponse<Device> {
        public DeviceResponse(Device device) : base(device) { }
        public DeviceResponse(string message) : base(message) { }
    }
}
