using Swashbuckle.AspNetCore.Filters;

namespace MoneyMe.API.Application.Shared.Examples
{
    public class RegisterCustomerReponse
    {
        public string data { get; set; }
        public dynamic? meta { get; set; }
    }

    public class RegisterCustomerResponseExample : IExamplesProvider<RegisterCustomerReponse>
    {
        private readonly IConfiguration _configuration;

        public RegisterCustomerResponseExample(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RegisterCustomerReponse GetExamples()
        {
            return new RegisterCustomerReponse
            {
                data = $"{_configuration.GetSection("ExternalUrl").GetValue<string>("BaseUrl")}326CA8AB-A464-4FA0-969C-65617F70513D",
                meta = null
            };
        }
    }
}
