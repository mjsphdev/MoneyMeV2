using Microsoft.EntityFrameworkCore;
using MoneyMe.API.Infrastructure.Persistence;
using MoneyMe.API.Models;

namespace MoneyMe.API.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public CustomerRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }
        public async Task<string> AddCustomerAsync(Customer customer)
        {
             _appDbContext.Customers.Add(customer);
            await _appDbContext.SaveChangesAsync();

            var _externalUrl = _configuration.GetSection("ExternalUrl").GetValue<string>("BaseUrl");

            return $"{_externalUrl}{customer.CustomerId}";
        }

        public async Task<int> ApplyLoanAsync(Loan loan)
        {
            _appDbContext.Loans.Add(loan);
            await _appDbContext.SaveChangesAsync();

            return loan.LoanId;
        }

        public async Task<Customer?> GetCustomerByIdAsync(string customerId)
        {
            return await _appDbContext.Customers.Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            var _updateEntity = await _appDbContext.Customers.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefaultAsync();

           if(_updateEntity != null)
           {
               _updateEntity.CustomerId = customer.CustomerId;
               _updateEntity.AmountRequired = customer.AmountRequired;
               _updateEntity.Term = customer.Term;
               _updateEntity.FirstName = customer.FirstName;
               _updateEntity.LastName = customer.LastName;
               _updateEntity.Email = customer.Email;
               _updateEntity.Mobile = customer.Mobile;
               _updateEntity.DateOfBirth = customer.DateOfBirth;
               
               _appDbContext.Customers.Update(_updateEntity);
               await _appDbContext.SaveChangesAsync();
           }


            return customer.Id;
        }
    }
}
