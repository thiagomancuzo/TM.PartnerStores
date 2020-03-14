namespace TM.PartnerStores.Application.Partner
{
    using System;
    using System.Threading.Tasks;
    using TM.PartnerStores.Application.Contracts;
    using TM.PartnerStores.Application.Operations;
    using TM.PartnerStores.Application.Operations.IO;
    using TM.PartnerStores.Application.Partner.Models.PartnerCreation;
    using TM.PartnerStores.Application.Partner.Models.PartnerList;
    using TM.PartnerStores.Application.Partner.Models.SinglePartnerRetrieve;
    using TM.PartnerStores.Domain.Exceptions;
    using TM.PartnerStores.Domain.Repositories;

    public class PartnerApplicationService : IPartnerApplicationService
    {
        private readonly IPartnerApplicationServiceParser _parser;
        private readonly IPartnerRepository _partnerRepository;

        public PartnerApplicationService(IPartnerApplicationServiceParser parser, IPartnerRepository partnerRepository)
        {
            _parser = parser;
            _partnerRepository = partnerRepository;
        }

        public async Task<IApplicationOperation<PartnerCreationOutput>> CreateAsync(PartnerCreationInput input)
        {
            return await HandleOperation<PartnerCreationOutput>(async () =>
            {
                var partner = _parser.FromPartnerCreationInput(input);
                var alreadyCreatedPartner = await _partnerRepository.GetAsync(partner.Document).ConfigureAwait(false);
                if (alreadyCreatedPartner != null) throw new AlreadyCreatedPartnerException(partner);

                alreadyCreatedPartner = await _partnerRepository.GetAsync(partner.Id).ConfigureAwait(false);
                if (alreadyCreatedPartner != null) throw new AlreadyCreatedPartnerException(partner);

                await _partnerRepository.CreateAsync(partner);

                return new PartnerCreationOutput();
            });
        }

        public async Task<IApplicationOperation<SinglePartnerRetrieveOutput>> GetByIdAsync(int id)
        {
            return await HandleOperation<SinglePartnerRetrieveOutput>(async () =>
            {
                var partner = await _partnerRepository.GetAsync(id);
                return _parser.ToSinglePartnerRetrieveOutput(partner);
            });
        }

        public async Task<IApplicationOperation<PartnerListOutput>> ListAsync()
        {
            return await HandleOperation<PartnerListOutput>(async () =>
            {
                var partners = await _partnerRepository.GetAsync();
                return _parser.ToPartnerListOutput(partners);
            });
        }

        public async Task<IApplicationOperation<PartnerSearchOutput>> SearchAsync(PartnerSearchInput input)
        {
            return await HandleOperation<PartnerSearchOutput>(async () =>
            {
                var partner = await _partnerRepository.GetNearstAsync(_parser.FromPartnerSearchInput(input));
                return _parser.ToPartnerSearchOutput(partner);
            });
        }

        private async Task<IApplicationOperation<T>> HandleOperation<T>(Func<Task<T>> operation) where T : IOutput
        {
            try
            {
                var result = await operation();
                if (result == null) return new NullResultOperation<T>();
                return new SuccessfulOperation<T>(result);
            }
            catch (DomainException ex)
            {
                return new ErrorOperation<T>(ex, OperationErrorType.InvalidInput);
            }
            catch (Exception ex)
            {
                return new ErrorOperation<T>(ex, OperationErrorType.Unknown);
            }
        }
    }
}
