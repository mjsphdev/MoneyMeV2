using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.API.Application.Commands;
using MoneyMe.API.Application.Queries;

namespace MoneyMe.API.Controllers
{
    [Route("api/loan")]
    [ApiController]
    public class LoanController : BaseController
    {
        private readonly ISender _sender;

        public LoanController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{customer_id}")]
        public async Task<IActionResult> GetCustomer(string customer_id, CancellationToken ct = default)
        {
            var request = new Loan_GetApplicantQuery { customerid = customer_id };

            var result = await _sender.Send(request, ct);

            return OkResult(result);
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyLoan([FromBody] ApplyLoan_Command request, CancellationToken ct = default)
        {
            var result = await _sender.Send(request, ct);

            return OkResult(result);
        }
    }
}
