namespace TM.PartnerStores.Domain.Partner.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TM.PartnerStores.Domain.Exceptions;
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

        public async Task CreateAsync(Partner partner)
        {
            var alreadyCreatedPartner = await _partnerRepository.GetAsync(partner.Document).ConfigureAwait(false);
            if(alreadyCreatedPartner != null) throw new AlreadyCreatedPartnerException(partner); 

            alreadyCreatedPartner = await _partnerRepository.GetAsync(partner.Id).ConfigureAwait(false);
            if(alreadyCreatedPartner != null) throw new AlreadyCreatedPartnerException(partner); 

            await _partnerRepository.CreateAsync(partner);
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
