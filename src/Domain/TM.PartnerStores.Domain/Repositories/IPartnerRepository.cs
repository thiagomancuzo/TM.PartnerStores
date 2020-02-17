namespace TM.PartnerStores.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TM.PartnerStores.Domain.Partner.Entities;

    public interface IPartnerRepository
    {
        Task<IEnumerable<Partner>> GetAsync();

        Task<Partner> GetAsync(int id);

        Task<Partner> GetNearstAsync(Point location);

        Task CreateAsync(Partner partner);
    }
}
