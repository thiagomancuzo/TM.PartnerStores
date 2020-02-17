namespace TM.PartnerStores.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TM.PartnerStores.Domain.Partner.Entities;

    public interface IPartnerService
    {
        Task CreateAsync(Partner partner);

        Task<IEnumerable<Partner>> ListAsync();

        Task<Partner> SearchAsync(Point point);

        Task<Partner> GetByIdAsync(int id);
    }
}
