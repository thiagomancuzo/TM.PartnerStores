namespace TM.PartnerStores.Domain.Partner.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Multipolygon : IEnumerable<Polygon>
    {
        private readonly IEnumerable<Polygon> polygon;

        public Multipolygon(IEnumerable<Polygon> polygon) =>
            this.polygon = polygon ?? throw new ArgumentNullException(nameof(polygon));

        public Multipolygon(IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> values) =>
            polygon = values?.Select(v => new Polygon(v)) ?? throw new ArgumentNullException(nameof(values));

        public IEnumerator<Polygon> GetEnumerator()
        {
            return polygon.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return polygon.GetEnumerator();
        }
    }
}
