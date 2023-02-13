using FluentValidation;
using MediatR;
using MoneyMe.API.Application.Shared.Exceptions;

namespace MoneyMe.API.Application.Shared.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse>
            : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var context = new ValidationContext<TRequest>(request);

                var failures = _validators
                    .Select(v =>
                    {
                        return v.ValidateAsync(context).Result;
                    })
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .Select(i => i.ErrorMessage)
                    .ToList();

                if (failures.Any())
                {
                    throw new InvalidParameterException(failures);
                }

                return await next();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
