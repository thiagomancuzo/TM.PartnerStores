namespace TM.PartnerStores.Repository.MongoDB.Model
{
    using global::MongoDB.Bson;
    using global::MongoDB.Driver.GeoJsonObjectModel;
 
    public class PartnerModel : BaseModel
    {
        public ObjectId _id { get; set; }

        public int ApplicationId { get; set; }

        public string TradingName { get; set; }

        public string OwnerName { get; set; }

        public string Document { get; set; }

        public GeoJsonMultiPolygon<GeoJson2DCoordinates> CoverageArea { get; set; }

        public GeoJsonPoint<GeoJson2DCoordinates> Address { get; set; }
    }
}
