using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Gateways.Mapping {
    public class CreateGatewayResource : IValidatableObject {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        public string IP { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (!IPAddress.TryParse(IP, out IPAddress _)) yield return new ValidationResult("Invalid IP address.");
            yield break;
        }
    }
}
