using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BankAccountServiceAPI.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (Equals(!_validators.Any()))
            {
                //Если для запроса нет валидаторов, просто передаём управление дальше
                return await next();
            }

            ValidationContext<TRequest> validationContext = new ValidationContext<TRequest>(request);

            //Логика нахождения всех ошибок валидации из всех валидаторов для данного запроса
            List<ValidationFailure> validationFailures = (await Task.WhenAll(
                   _validators.Select(v => v.ValidateAsync(validationContext, cancellationToken))))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (validationFailures.Any())
            {
                //Если нашлись ошибки валидации, затем превращаем их формат mbError.
                List<MbError> errors = validationFailures
                    .Select(f => new MbError(f.ErrorCode, f.ErrorMessage))
                    .ToList();

                var responseType = typeof(TResponse);

                if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(MbResult<>))
                {
                    var genericArgument = responseType.GetGenericArguments()[0];
                    var resultType = typeof(MbResult<>).MakeGenericType(genericArgument);
                    var failureMethod = resultType.GetMethod(nameof(MbResult<object>.Failure),new[] { typeof(IEnumerable<MbError>) });
                    return (TResponse)failureMethod.Invoke(null, new object[] { errors });
                }
                else if(responseType == typeof(MbResult))
                {
                    var failureMethod = typeof(MbResult).GetMethod(nameof(MbResult.Failure), new[] { typeof(IEnumerable<MbError>) });
                    return (TResponse)failureMethod.Invoke(null, new object[] { errors });
                }
            }
            return await next();
        }
    }
}
