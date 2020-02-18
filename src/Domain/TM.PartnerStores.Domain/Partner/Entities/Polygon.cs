namespace TM.PartnerStores.Domain.Partner.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TM.PartnerStores.Domain.Exceptions;

    public class Polygon : IEnumerable<IEnumerable<Point>>
    {
        private readonly List<Point> points;
        private readonly List<List<Point>> pointWrapper;

        public Polygon(IEnumerable<Point> points)
        {
            this.points = points?.ToList() ?? throw new ArgumentNullException(nameof(points));
            this.pointWrapper = new List<List<Point>>
            {
                new List<Point>(points)
            };

            ThrowIfInvalidPolygon(this.points);
        }

        public Polygon(IEnumerable<IEnumerable<IEnumerable<double>>> values)
        {
            points = values?.First().Select(v => new Point(v)).ToList() ?? throw new ArgumentNullException(nameof(values));
            this.pointWrapper = new List<List<Point>>
            {
                new List<Point>(points)
            };

            ThrowIfInvalidPolygon(points);
        }

        // TODO : check if this validation rules make sense
        private void ThrowIfInvalidPolygon(List<Point> points)
        {
            if (points.Count < 4) throw new InvalidPolygonException("A polygon must to have at least 4 points");
            if (!points[0].Equals(points[points.Count - 1])) throw new InvalidPolygonException("The first point of a polygon must to be equals the first one"); ;
        }

        public IEnumerator<IEnumerable<Point>> GetEnumerator()
        {
            return pointWrapper.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return pointWrapper.GetEnumerator();
        }
    }
}
