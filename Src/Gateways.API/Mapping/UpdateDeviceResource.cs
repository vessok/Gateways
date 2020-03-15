using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gateways.Mapping {
    public class UpdateDeviceResource : IValidatableObject {
        [Required]
        [MaxLength(50)]
        public string Vendor { get; set; }
        
        [Required]
        [MaxLength(10)]
        public String Status { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (Status != "OnLine" && Status != "OffLine") yield return new ValidationResult("Invalid status (OnLine/OffLine).");
            yield break;
        }
    }
}
