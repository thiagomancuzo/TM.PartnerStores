using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TM.PartnerStores.Application.Contracts;
using TM.PartnerStores.Application.Operations;
using TM.PartnerStores.Application.Partner.Models.PartnerCreation;
using TM.PartnerStores.Application.Partner.Models.SinglePartnerRetrieve;

namespace TM.PartnerStores.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartnersController : ControllerBase
    {

        private readonly ILogger<PartnersController> _logger;
        private readonly IPartnerApplicationService _partnerApplicationService;

        public PartnersController(IPartnerApplicationService partnerApplicationService, ILogger<PartnersController> logger)
        {
            _logger = logger;
            _partnerApplicationService = partnerApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var operationResult = await _partnerApplicationService.ListAsync();
            if (operationResult.Successful)
            {
                return Ok(operationResult.Result);
            }
            
            return ErrorResponse((IErrorDescriptor)operationResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var operationResult = await _partnerApplicationService.GetByIdAsync(id);
            if (operationResult.Successful)
            {
                return Ok(operationResult.Result);
            }

            return ErrorResponse((IErrorDescriptor)operationResult);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Get([FromQuery]PartnerSearchInput input)
        {
            var operationResult = await _partnerApplicationService.SearchAsync(input);
            if (operationResult.Successful)
            {
                return Ok(operationResult.Result);
            }

            return ErrorResponse((IErrorDescriptor)operationResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartnerCreationInput input)
        {
            var operationResult = await _partnerApplicationService.CreateAsync(input);
            if (operationResult.Successful)
            {
                return Ok(operationResult.Result);
            }

            return ErrorResponse((IErrorDescriptor)operationResult);
        }

        private IActionResult ErrorResponse(IErrorDescriptor descriptor) =>
            descriptor.OperationErrorType switch
            {
                OperationErrorType.InvalidInput => BadRequest(descriptor.Message),
                OperationErrorType.NullResult => NotFound(descriptor.Message),
                _ => StatusCode(500, descriptor.Message)
            };
    }
}
