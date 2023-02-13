using MediatR;
using MoneyMe.API.Models;
using MoneyMe.API.Infrastructure.Repositories;
using MoneyMe.API.Application.Shared.Interfaces;

namespace MoneyMe.API.Application.Commands
{
    public class RegisterCustomer_Command : IRequest<string>
    {
        public Decimal amountrequired { get; set; }
        public int term { get; set; }
        public string title { get; set; } = string.Empty;
        public string firstname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
        public string dateofbirth { get; set; } = string.Empty;
        public string mobile { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }

    public class RegisterCustomer_CommandHandler : IRequestHandler<RegisterCustomer_Command, string>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDateTime _dateTime;

        public RegisterCustomer_CommandHandler(ICustomerRepository customerRepository, IDateTime dateTime)
        {
            _customerRepository = customerRepository;
            _dateTime = dateTime;
        }

        public async Task<string> Handle(RegisterCustomer_Command request, CancellationToken ct)
        {
            var customer = new Customer
            {
                AmountRequired = request.amountrequired,
                Term = request.term,
                Title = request.title,
                FirstName = request.firstname,
                LastName = request.lastname,
                DateOfBirth = string.IsNullOrEmpty(request.dateofbirth) ? _dateTime.Default : Convert.ToDateTime(request.dateofbirth),
                Mobile = request.mobile,
                Email = request.email
            };

            return await _customerRepository.AddCustomerAsync(customer);
        }
    }
}
