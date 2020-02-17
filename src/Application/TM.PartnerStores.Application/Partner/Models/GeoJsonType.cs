using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TM.PartnerStores.Application.Partner.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GeoJsonType
    {
        [EnumMember(Value = "Point")]
        Point,
        [EnumMember(Value = "MultiPolygon")]
        Multipolygon
    }
}
