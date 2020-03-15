using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gateways.Persistance;

namespace Gateways.Mapping {
    public class CreateDeviceResource : UpdateDeviceResource {
        [Required]
        [MaxLength(38)]
        public string GatewayId { get; set; }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            var _gatewayRepo = (IGatewayRepo)validationContext.GetService(typeof(IGatewayRepo));
            var gateway = _gatewayRepo.FindByIdAsync(GatewayId).Result;
            if (gateway == null) {
                yield return new ValidationResult("Gateway not found.");
            }// else if (gateway.Devices.Count >= 9) {
            //    yield return new ValidationResult("No more than 10 devices allowed per gateway.");
            //}

            foreach(var result in base.Validate(validationContext)) yield return result;
        }
    }
}
