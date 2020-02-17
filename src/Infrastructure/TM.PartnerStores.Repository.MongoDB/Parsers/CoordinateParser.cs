namespace TM.PartnerStores.Repository.MongoDB.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::MongoDB.Driver.GeoJsonObjectModel;
    using TM.PartnerStores.Domain.Partner.Entities;

    public class CoordinateParser : ICoordinateParser
    {
        public Point ToDomain(GeoJsonPoint<GeoJson2DCoordinates> point)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));
            
            return new Point(point.Coordinates.X, point.Coordinates.Y);
        }

        public GeoJsonPoint<GeoJson2DCoordinates> ToModel(Point point)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));

            return new GeoJsonPoint<GeoJson2DCoordinates>(
                new GeoJson2DCoordinates(point.Lat, point.Lng)
                );
        }

        public Multipolygon ToDomain(GeoJsonMultiPolygon<GeoJson2DCoordinates> multipolygon)
        {
            if (multipolygon == null) throw new ArgumentNullException(nameof(multipolygon));

            var polygons = new List<Polygon>();

            foreach(var mp in multipolygon.Coordinates.Polygons)
            {
                var points = new List<Point>();

                foreach(var p in mp.Exterior.Positions)
                {
                    points.Add(new Point(p.X, p.Y));
                }

                polygons.Add(new Polygon(points));
            }

            return new Multipolygon(polygons);
        }

        public GeoJsonMultiPolygon<GeoJson2DCoordinates> ToModel(Multipolygon multipolygon)
        {
            if (multipolygon == null) throw new ArgumentNullException(nameof(multipolygon));

            var geoJsonPolygonCoordinatesList = new List<GeoJsonPolygonCoordinates<GeoJson2DCoordinates>>();

            foreach (var polygon in multipolygon)
            {
                var geoJson2DCoordinatesList = new List<GeoJson2DCoordinates>();

                foreach(var point in polygon.First())
                {
                    geoJson2DCoordinatesList.Add(new GeoJson2DCoordinates(point.Lat, point.Lng));
                }

                geoJsonPolygonCoordinatesList.Add(
                    new GeoJsonPolygonCoordinates<GeoJson2DCoordinates>(
                        new GeoJsonLinearRingCoordinates<GeoJson2DCoordinates>(
                            geoJson2DCoordinatesList
                            )
                        )
                    );
            }

            return new GeoJsonMultiPolygon<GeoJson2DCoordinates>(new GeoJsonMultiPolygonCoordinates<GeoJson2DCoordinates>(geoJsonPolygonCoordinatesList));
        }
    }
}
