namespace TM.PartnerStores.Application.Partner.Models
{
    using System.Collections;

    public class GeoJson<T> where T : IEnumerable
    {
        public GeoJsonType Type { get; set; }

        public T Coordinates { get; set; }
    }
}
