namespace TM.PartnerStores.Domain.Partner.Entities
{
    using System;

    public class Partner
    {
        public Partner(int id, string tradingName, string ownerName, Document document, Multipolygon coverageArea, Point address)
        {
            // ensures de domaind integrity

            if (id == default(int)) throw new ArgumentException("Id parameter must to have a non-zero value", nameof(id));

            Id = id;
            TradingName = tradingName ?? throw new ArgumentNullException(nameof(tradingName));
            OwnerName = ownerName ?? throw new ArgumentNullException(nameof(ownerName));
            Document = document;
            CoverageArea = coverageArea ?? throw new ArgumentNullException(nameof(coverageArea));
            Address = address ?? throw new ArgumentNullException(nameof(address)); ;
        }

        public int Id { get; }

        public string TradingName { get; }

        public string OwnerName { get; }

        public Document Document { get; }

        public Multipolygon CoverageArea { get; }

        public Point Address { get; }
    }
}
