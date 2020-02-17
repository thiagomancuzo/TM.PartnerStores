namespace TM.PartnerStores.Application.Contracts
{
    using System.Collections.Generic;
    using TM.PartnerStores.Application.Partner.Models.PartnerCreation;
    using TM.PartnerStores.Application.Partner.Models.PartnerList;
    using TM.PartnerStores.Application.Partner.Models.SinglePartnerRetrieve;
    using TM.PartnerStores.Domain.Partner.Entities;

    public interface IPartnerApplicationServiceParser
    {
        Partner FromPartnerCreationInput(PartnerCreationInput partner);

        PartnerListOutput ToPartnerListOutput(IEnumerable<Partner> partners);

        SinglePartnerRetrieveOutput ToSinglePartnerRetrieveOutput(Partner partner);

        PartnerSearchOutput ToPartnerSearchOutput(Partner partner);

        Point FromPartnerSearchInput(PartnerSearchInput partner);
    }
}
