using FluentValidation;
using MediatR;
using MoneyMe.API.Application.Shared.Interfaces;
using MoneyMe.API.Infrastructure.Repositories;
using MoneyMe.API.Models;

namespace MoneyMe.API.Application.Commands
{
    public class ApplyLoan_Command : IRequest<int>
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public Decimal AmountRequired { get; set; }
        public int Term { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Decimal RepaymentAmount { get; set; }
        public Decimal TotalInterest { get; set; }
        public Decimal EstablishmentFee { get; set; }
        public Decimal RepaymentsFrom { get; set; }
    }

    public class ApplyLoan_CommandValidator : AbstractValidator<ApplyLoan_Command>
    {
        public ApplyLoan_CommandValidator(IDateTime dateTime)
        {
            RuleFor(x => x.DateOfBirth)
                    .Must(dateOfBirth => dateTime.Now.Subtract(Convert.ToDateTime(dateOfBirth)).TotalDays / 365.25 >= 18)
                    .WithMessage("Applicant must be at least 18 years old.");
        }
    }

    public class ApplyLoan_CommandHandler : IRequestHandler<ApplyLoan_Command, int>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDateTime _dateTime;

        public ApplyLoan_CommandHandler(ICustomerRepository customerRepository, IDateTime dateTime)
        {
            _customerRepository = customerRepository;
            _dateTime = dateTime;
        }
        public async Task<int> Handle(ApplyLoan_Command request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = request.Id,
                CustomerId = request.CustomerId,
                AmountRequired = request.AmountRequired,
                Term = request.Term,
                Title = request.Title,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = string.IsNullOrEmpty(request.DateOfBirth) ? _dateTime.Default : Convert.ToDateTime(request.DateOfBirth),
                Mobile = request.Mobile,
                Email = request.Email
            };

            await _customerRepository.UpdateCustomerAsync(customer);

            var loan = new Loan
            {
                CustomerId = request.CustomerId,
                TotalRepayment = request.RepaymentAmount,
                TotalInterest = request.TotalInterest,
                EstablishmentFee = request.EstablishmentFee,
                RepaymentsFrom = request.RepaymentsFrom,
            };
            
            return await _customerRepository.ApplyLoanAsync(loan);

        }
    }
}
