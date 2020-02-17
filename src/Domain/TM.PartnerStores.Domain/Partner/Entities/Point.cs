namespace TM.PartnerStores.Domain.Partner.Entities
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TM.PartnerStores.Domain.Exceptions;

    public class Point : IEnumerable<double>
    {

        private readonly List<double> values;

        public Point(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;

            values = new List<double>(new[] { lat, lng });
        }

        public Point(IEnumerable<double> values)
        {
            var valuesList = values.ToList();

            if (valuesList.Count != 2) throw new InvalidPointException($"The values array has an invalid length. The Point values parameter length must to be equals 2.");

            Lat = valuesList[0];
            Lng = valuesList[1];
        }

        public double Lat { get; private set; }

        public double Lng { get; private set; }

        public IEnumerator<double> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Point;
            if (other is null) return false;

            return this.Lat == other.Lat
                && this.Lng == other.Lng;
        }

        public static bool operator ==(Point left, Point right) => left.Equals(right);

        public static bool operator !=(Point left, Point right) => !left.Equals(right);
    }
}