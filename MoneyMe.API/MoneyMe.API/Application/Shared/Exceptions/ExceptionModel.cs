namespace MoneyMe.API.Application.Shared.Exceptions
{
    public class ExceptionModel
    {
        public string exceptiontype { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public object? exceptiondetails { get; set; }
        public Newtonsoft.Json.Linq.JObject? apiresponse { get; set; }
    }
}
