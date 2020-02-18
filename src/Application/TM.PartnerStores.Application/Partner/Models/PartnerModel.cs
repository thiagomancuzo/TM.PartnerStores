namespace TM.PartnerStores.Application.Partner.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PartnerModel : IValidatableObject
    {
        public int Id { get; set; }

        public string TradingName { get; set; }

        public string OwnerName { get; set; }

        public string Document { get; set; }

        public GeoJson<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>> CoverageArea { get; set; }

        public GeoJson<IEnumerable<double>> Address { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Id == default(int)) yield return new ValidationResult("The Id field is required");
            if(string.IsNullOrEmpty(TradingName)) yield return new ValidationResult("The TradingName field is required");
            if(string.IsNullOrEmpty(OwnerName)) yield return new ValidationResult("The OwnerName field is required");
            if(string.IsNullOrEmpty(Document)) yield return new ValidationResult("The Document field is required");
            if(CoverageArea is null) yield return new ValidationResult("The CoverageArea field is required");
            if(Address is null) yield return new ValidationResult("The Address field is required");
        }
    }
}
