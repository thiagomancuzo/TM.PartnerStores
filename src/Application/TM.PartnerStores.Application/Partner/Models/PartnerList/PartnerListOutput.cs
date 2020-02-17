namespace TM.PartnerStores.Application.Partner.Models.PartnerList
{
    using System.Collections.Generic;
    using TM.PartnerStores.Application.Operations.IO;

    public class PartnerListOutput : IOutput
    {
        public PartnerListOutput()
        {
            Pdvs = new List<PartnerModel>();
        }

        public List<PartnerModel> Pdvs { get; set; }
    }
}
