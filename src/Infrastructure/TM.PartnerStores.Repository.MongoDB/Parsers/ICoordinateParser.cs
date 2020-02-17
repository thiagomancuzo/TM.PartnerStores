namespace TM.PartnerStores.Repository.MongoDB.Parsers
{
    using global::MongoDB.Driver.GeoJsonObjectModel;
    using TM.PartnerStores.Domain.Partner.Entities;

    public interface ICoordinateParser
    {
        GeoJsonPoint<GeoJson2DCoordinates> ToModel(Point point);
        
        Point ToDomain(GeoJsonPoint<GeoJson2DCoordinates> point);

        GeoJsonMultiPolygon<GeoJson2DCoordinates> ToModel(Multipolygon multipolygon);

        Multipolygon ToDomain(GeoJsonMultiPolygon<GeoJson2DCoordinates> multipolygon);
    }
}
