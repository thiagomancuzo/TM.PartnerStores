namespace TM.PartnerStores.Application.Contracts
{
    using System.Threading.Tasks;
    using TM.PartnerStores.Application.Partner.Models.PartnerCreation;
    using TM.PartnerStores.Application.Partner.Models.PartnerList;
    using TM.PartnerStores.Application.Partner.Models.SinglePartnerRetrieve;

    public interface IPartnerApplicationService
    {
        Task<IApplicationOperation<PartnerCreationOutput>> CreateAsync(PartnerCreationInput input);

        Task<IApplicationOperation<PartnerListOutput>> ListAsync();

        Task<IApplicationOperation<PartnerSearchOutput>> SearchAsync(PartnerSearchInput input);

        Task<IApplicationOperation<SinglePartnerRetrieveOutput>> GetByIdAsync(int id);
    }
}
