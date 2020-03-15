using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Gateways.Mapping;
using Gateways.Model;
using Gateways.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gateways.Controllers {
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DevicesController : ControllerBase {
        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;
        
        public DevicesController(IDeviceService deviceService, IMapper mapper) {
            _deviceService = deviceService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeviceResource>), 200)]
        public async Task<IEnumerable<DeviceResource>> ListGatewaysAsync([FromQuery] QueryDeviceResource queryResource) {
            QueryDevice query = _mapper.Map<QueryDevice>(queryResource);
            var queryResult = await _deviceService.FindAsync(query);
            var resource = _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceResource>>(queryResult);
            return resource;
        }

        [HttpGet]
        [Route("with_gateway")]
        [ProducesResponseType(typeof(IEnumerable<DeviceWithGatewayResources>), 200)]
        public async Task<IEnumerable<DeviceWithGatewayResources>> ListGatewaysWithDevicesAsync([FromQuery] QueryDeviceResource queryResource) {
            QueryDevice query = _mapper.Map<QueryDevice>(queryResource);
            var queryResult = await _deviceService.FindAsync(query);
            var resource = _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceWithGatewayResources>>(queryResult);
            return resource;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeviceResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] CreateDeviceResource resource) {
            var device = _mapper.Map<Device>(resource);
            var result = await _deviceService.CreateAsync(device);
            
            if (!result.Success) {
                return BadRequest(new ErrorResource(result.Message));
            }

            var deviceResource = _mapper.Map<DeviceResource>(result.Resource);
            return Ok(deviceResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DeviceResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateDeviceResource resource) {
            var device = _mapper.Map<Device>(resource);
            var result = await _deviceService.UpdateAsync(id, device);

            if (!result.Success) {
                return BadRequest(new ErrorResource(result.Message));
            }

            var gatewayResource = _mapper.Map<GatewayResource>(result.Resource);
            return Ok(gatewayResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeviceResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id) {
            var result = await _deviceService.DeleteAsync(id);

            if (!result.Success) {
                return BadRequest(new ErrorResource(result.Message));
            }

            var gatewayResource = _mapper.Map<DeviceResource>(result.Resource);
            return Ok(gatewayResource);
        }
    }
}