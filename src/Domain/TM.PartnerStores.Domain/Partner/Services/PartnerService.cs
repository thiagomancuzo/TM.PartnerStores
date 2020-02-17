namespace TM.PartnerStores.Domain.Partner.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TM.PartnerStores.Domain.Partner.Entities;
    using TM.PartnerStores.Domain.Repositories;
    using TM.PartnerStores.Domain.Services;

    public class PartnerService : IPartnerService
    {
        private readonly IPartnerRepository _partnerRepository;

        public PartnerService(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public Task CreateAsync(Partner partner)
        {
            return _partnerRepository.CreateAsync(partner);
        }

        public Task<Partner> GetByIdAsync(int id)
        {
            return _partnerRepository.GetAsync(id);
        }

        public Task<IEnumerable<Partner>> ListAsync()
        {
            return _partnerRepository.GetAsync();
        }

        public Task<Partner> SearchAsync(Point point)
        {
            return _partnerRepository.GetNearstAsync(point);
        }
    }
}
