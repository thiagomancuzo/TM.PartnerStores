namespace TM.PartnerStores.Domain.Partner.Entities
{
    public class Partner
    {
        public Partner(int id, string tradingName, string ownerName, Document document, Multipolygon coverageArea, Point address)
        {
            Id = id;
            TradingName = tradingName;
            OwnerName = ownerName;
            Document = document;
            CoverageArea = coverageArea;
            Address = address;
        }

        public int Id { get; }

        public string TradingName { get; }

        public string OwnerName { get; }

        public Document Document { get; }

        public Multipolygon CoverageArea { get; }

        public Point Address { get; }
    }
}
