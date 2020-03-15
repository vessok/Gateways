using Gateways.Model;

namespace Gateways.Services {
    public class GatewayResponse : BaseResponse<Gateway> {
        public GatewayResponse(Gateway gateway) : base(gateway) { }
        public GatewayResponse(string message) : base(message) { }
    }
}
