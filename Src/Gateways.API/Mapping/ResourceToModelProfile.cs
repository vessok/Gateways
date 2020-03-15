using AutoMapper;
using Gateways.Model;
using System.Net;

namespace Gateways.Mapping {
    public class ResourceToModelProfile : Profile {
        public ResourceToModelProfile() {
            CreateMap<QueryDeviceResource, QueryDevice>();
            CreateMap<QueryGatewayResource, QueryGateway>();

            CreateMap<CreateGatewayResource, Gateway>()
                .ForMember(d => d.IP, memberOptions => memberOptions.MapFrom(src => IPAddress.Parse(src.IP)));
            
            CreateMap<CreateDeviceResource, Device>();
            CreateMap<UpdateDeviceResource, Device>();
        }
    }
}
