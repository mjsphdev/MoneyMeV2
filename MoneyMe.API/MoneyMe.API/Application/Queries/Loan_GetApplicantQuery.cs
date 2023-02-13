using MediatR;
using MoneyMe.API.Models;
using MoneyMe.API.Infrastructure.Repositories;

namespace MoneyMe.API.Application.Queries
{
    public class Loan_GetApplicantQuery : IRequest<Customer?>
    {
        public string customerid { get; set; } = string.Empty;
    }

    public class Loan_GetApplicantQueryHandler : IRequestHandler<Loan_GetApplicantQuery, Customer?>
    {
        private readonly ICustomerRepository _customerRepository;

        public Loan_GetApplicantQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Customer?> Handle(Loan_GetApplicantQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomerByIdAsync(request.customerid);
        }
    }
}
