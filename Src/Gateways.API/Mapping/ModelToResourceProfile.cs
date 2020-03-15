using AutoMapper;
using Gateways.Model;
using Gateways.Persistance;

namespace Gateways.Mapping {
    public class ModelToResourceProfile  : Profile {
        public ModelToResourceProfile() {
            CreateMap<Gateway, GatewayResource>()
                .ForMember(d => d.IP, memberOptions => memberOptions.MapFrom(s => s.IPAddress.ToString()));

            CreateMap<Gateway, GatewayWithDevicesResource>()
               .ForMember(d => d.IP, memberOptions => memberOptions.MapFrom(s => s.IPAddress.ToString()));

            CreateMap<Device, DeviceResource>()
                .ForMember(d => d.Status, memberOptions => memberOptions.MapFrom(s => s.Status.ToString()));

            CreateMap<Device, DeviceWithGatewayResources>()
                .ForMember(d => d.Status, memberOptions => memberOptions.MapFrom(s => s.Status.ToString()));
        }
    }
}
