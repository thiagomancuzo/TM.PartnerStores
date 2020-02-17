namespace TM.PartnerStores.Application.Partner.Models
{
    using System.Collections.Generic;

    public class PartnerModel
    {
        public int Id { get; set; }

        public string TradingName { get; set; }

        public string OwnerName { get; set; }

        public string Document { get; set; }

        public GeoJson<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>> CoverageArea { get; set; }

        public GeoJson<IEnumerable<double>> Address { get; set; }
    }
}
