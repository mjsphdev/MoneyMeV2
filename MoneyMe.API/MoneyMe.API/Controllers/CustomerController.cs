using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.API.Application.Commands;
using MoneyMe.API.Application.Shared.Examples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace MoneyMe.API.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ISender _sender;

        public CustomerController(ISender sender)
        {
            _sender = sender;
        }

        [SwaggerRequestExample(typeof(RegisterCustomerRequest), typeof(RegisterCustomerRequestExample))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(RegisterCustomerReponse))]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomer_Command request, CancellationToken ct = default)
        {
            var result = await _sender.Send(request, ct);

            return OkResult(result);
        }
    }
}
