namespace TM.PartnerStores.Application.Partner.Models.SinglePartnerRetrieve
{
    using TM.PartnerStores.Application.Operations.IO;

    public class PartnerSearchInput : IInput
    {
        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}
