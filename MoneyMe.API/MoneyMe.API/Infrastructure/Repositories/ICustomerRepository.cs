using MoneyMe.API.Models;

namespace MoneyMe.API.Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
       Task<Customer?> GetCustomerByIdAsync(string customerId);
       Task<string> AddCustomerAsync(Customer customer);
       Task<int> UpdateCustomerAsync(Customer customer);
       Task<int> ApplyLoanAsync(Loan loan);
    }
}
