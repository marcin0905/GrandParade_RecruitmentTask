using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using CustomerService.Application.Contants;
using CustomerService.Application.Contracts.Services.Customer;
using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Enum;
using CustomerService.Application.Enum.Application;
using CustomerService.Application.Filters;
using CustomerService.Application.Mediator.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICustomerQueryService _customerQueryService;

        public CustomerController(
            IMediator mediator,
            IIndex<ServiceType, ICustomerQueryService> customerQueryServiceTypes)
        {
            _mediator = mediator;
            _customerQueryService = customerQueryServiceTypes[ServiceType.Cache];
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseCustomerDto[]>> GetCustomers()
        {
            return Ok(await _customerQueryService.GetCustomers());
        }

        [HttpGet("{customerId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseCustomerDto[]>> GetCustomers([FromRoute] Guid customerId)
        {
            var customer = await _customerQueryService.GetCustomer(customerId);

            if (null == customer)
            {
                return NotFound($"Customer ID {customerId} not found");
            }

            return Ok(customer);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(InvalidateCacheActionFilter), Arguments = new object [] { CacheKeyConstants.CustomersListCacheKey })]
        public async Task<IActionResult> RegisterCustomer([FromBody] BaseCustomerDto customerDto)
        {
            switch (customerDto.ApplicationType)
            {
                case ApplicationType.MrGreen:
                    await _mediator.Send(CommandMapper.CreateCommand(customerDto as MrGreenCustomerDto));
                    break;
                case ApplicationType.RedBet:
                    await _mediator.Send(CommandMapper.CreateCommand(customerDto as RedBetCustomerDto));
                    break;
                default:
                    return BadRequest($"{customerDto.ApplicationType} - Application Type is not supported");
            }

            return StatusCode((int) HttpStatusCode.Created);
        }
    }
}