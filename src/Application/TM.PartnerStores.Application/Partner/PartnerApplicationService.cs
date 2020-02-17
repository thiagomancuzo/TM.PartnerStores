namespace TM.PartnerStores.Application.Partner
{
    using System;
    using System.Threading.Tasks;
    using TM.PartnerStores.Application.Contracts;
    using TM.PartnerStores.Application.Operations;
    using TM.PartnerStores.Application.Partner.Models.PartnerCreation;
    using TM.PartnerStores.Application.Partner.Models.PartnerList;
    using TM.PartnerStores.Application.Partner.Models.SinglePartnerRetrieve;
    using TM.PartnerStores.Domain.Exceptions;
    using TM.PartnerStores.Domain.Services;

    public class PartnerApplicationService : IPartnerApplicationService
    {
        private readonly IPartnerApplicationServiceParser _parser;
        private readonly IPartnerService _partnerService;

        public PartnerApplicationService(IPartnerApplicationServiceParser parser, IPartnerService partnerService)
        {
            _parser = parser;
            _partnerService = partnerService;
        }

        public async Task<IApplicationOperation<PartnerCreationOutput>> CreateAsync(PartnerCreationInput input)
        {
            try
            {
                await _partnerService.CreateAsync(_parser.FromPartnerCreationInput(input));
                return new SuccessfulOperation<PartnerCreationOutput>(new PartnerCreationOutput());
            }
            catch(DomainException ex)
            {
                return new ErrorOperation<PartnerCreationOutput>(ex, OperationErrorType.InvalidInput);
            }
            catch(Exception ex)
            {
                return new ErrorOperation<PartnerCreationOutput>(ex, OperationErrorType.Unknown);
            }
        }

        public async Task<IApplicationOperation<SinglePartnerRetrieveOutput>> GetByIdAsync(int id)
        {
            try
            {
                var partner = await _partnerService.GetByIdAsync(id);
                if (partner == null) return new NullResultOperation<SinglePartnerRetrieveOutput>();
                return new SuccessfulOperation<SinglePartnerRetrieveOutput>(_parser.ToSinglePartnerRetrieveOutput(partner));
            }
            catch (DomainException ex)
            {
                return new ErrorOperation<SinglePartnerRetrieveOutput>(ex, OperationErrorType.InvalidInput);
            }
            catch (Exception ex)
            {
                return new ErrorOperation<SinglePartnerRetrieveOutput>(ex, OperationErrorType.Unknown);
            }
        }

        public async Task<IApplicationOperation<PartnerListOutput>> ListAsync()
        {
            try
            {
                var partners = await _partnerService.ListAsync();
                return new SuccessfulOperation<PartnerListOutput>(_parser.ToPartnerListOutput(partners));
            }
            catch (DomainException ex)
            {
                return new ErrorOperation<PartnerListOutput>(ex, OperationErrorType.InvalidInput);
            }
            catch (Exception ex)
            {
                return new ErrorOperation<PartnerListOutput>(ex, OperationErrorType.Unknown);
            }
        }

        public async Task<IApplicationOperation<PartnerSearchOutput>> SearchAsync(PartnerSearchInput input)
        {
            try
            {
                var partner = await _partnerService.SearchAsync(_parser.FromPartnerSearchInput(input));
                if (partner == null) return new NullResultOperation<PartnerSearchOutput>();
                return new SuccessfulOperation<PartnerSearchOutput>(_parser.ToPartnerSearchOutput(partner));
            }
            catch (DomainException ex)
            {
                return new ErrorOperation<PartnerSearchOutput>(ex, OperationErrorType.InvalidInput);
            }
            catch (Exception ex)
            {
                return new ErrorOperation<PartnerSearchOutput>(ex, OperationErrorType.Unknown);
            }
        }
    }
}
