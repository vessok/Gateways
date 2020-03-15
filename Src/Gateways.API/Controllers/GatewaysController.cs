using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Gateways.Services;
using Gateways.Mapping;
using Gateways.Model;

namespace Gateways.Controllers {
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class GatewaysController : ControllerBase {
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public GatewaysController(IGatewayService gatewayService, IMapper mapper) {
            _gatewayService = gatewayService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GatewayResource>), 200)]
        public async Task<IEnumerable<GatewayResource>> ListGatewaysAsync([FromQuery] QueryGatewayResource queryResource) {
            var query = _mapper.Map<QueryGateway>(queryResource);
            var queryResult = await _gatewayService.FindAsync(query);
            var resource = _mapper.Map<IEnumerable<Gateway>, IEnumerable<GatewayResource>>(queryResult);
            return resource;
        }

        [HttpGet]
        [Route("with_devices")]
        [ProducesResponseType(typeof(IEnumerable<GatewayWithDevicesResource>), 200)]
        public async Task<IEnumerable<GatewayWithDevicesResource>> ListGatewaysWithDevicesAsync([FromQuery] QueryGatewayResource queryResource) {
            var query = _mapper.Map<QueryGateway>(queryResource);
            var queryResult = await _gatewayService.FindAsync(query);
            var resource = _mapper.Map<IEnumerable<Gateway>, IEnumerable<GatewayWithDevicesResource>>(queryResult);
            return resource;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GatewayResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] CreateGatewayResource resource) {
            var gateway = _mapper.Map<Gateway>(resource);

            var result = await _gatewayService.CreateAsync(gateway);

            if (!result.Success) {
                return BadRequest(new ErrorResource(result.Message));
            }

            var gatewayResource = _mapper.Map<GatewayResource>(result.Resource);
            return Ok(gatewayResource);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GatewayResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(string id, [FromBody] CreateGatewayResource resource) {
            var gateway = _mapper.Map<Gateway>(resource);
            var result = await _gatewayService.UpdateAsync(id, gateway);

            if (!result.Success) {
                return BadRequest(new ErrorResource(result.Message));
            }

            var gatewayResource = _mapper.Map<GatewayResource>(result.Resource);
            return Ok(gatewayResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GatewayResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(string id) {
            var result = await _gatewayService.DeleteAsync(id);

            if (!result.Success) {
                return BadRequest(new ErrorResource(result.Message));
            }

            var gatewayResource = _mapper.Map<GatewayResource>(result.Resource);
            return Ok(gatewayResource);
        }
    }
}