using MoneyMe.API.Application.Shared.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MoneyMe.API.Application.Shared.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {

                var response = context.Response;
                response.ContentType = "application/json";
                JObject? exceptionDetails = null;
                JObject? apiResponse = new JObject();
                string descriptionError = error?.InnerException?.Message ?? string.Empty;

                switch (error)
                {
                    case InvalidParameterException ex:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        exceptionDetails = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(new { messages = ex.MessageList }));
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        //TODO: Log Error
                        break;
                }

                var result = JsonConvert.SerializeObject(new ExceptionModel
                {
                    exceptiontype = error?.GetType().Name ?? "",
                    message = error?.Message ?? "",
                    description = descriptionError,
                    exceptiondetails = exceptionDetails,
                    apiresponse = apiResponse
                });

                await response.WriteAsync(result);

            }
        }

    }
}
