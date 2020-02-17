namespace TM.PartnerStores.Repository.MongoDB.Parsers
{
    using TM.PartnerStores.Domain.Partner.Entities;
    using TM.PartnerStores.Repository.MongoDB.Model;

    public interface IPartnerParser
    {
        PartnerModel ToModel(Partner partner);
        Partner ToDomain(PartnerModel model);
    }
}
