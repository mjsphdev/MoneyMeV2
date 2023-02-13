using Swashbuckle.AspNetCore.Filters;

namespace MoneyMe.API.Application.Shared.Examples
{
    public class RegisterCustomerRequest
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

    public class RegisterCustomerRequestExample : IExamplesProvider<RegisterCustomerRequest>
    {
        public RegisterCustomerRequest GetExamples()
        {
            return new RegisterCustomerRequest
            {
               amountrequired = 5000,
               term = 2,
               title = "Mr.",
               firstname = "John",
               lastname = "doe",
               dateofbirth = "01/01/1900",
               mobile = "0422111333",
               email = "layton@flower.com.au"
            };
        }
    }
}
