using FluentValidation;
using MediatR;
// ReSharper disable SuggestVarOrType_Elsewhere
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        // ReSharper disable once ConvertToPrimaryConstructor Я считаю такой конструктор класса более читаемым.
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Проверяем, есть ли вообще валидаторы для этого запроса.
            if (!_validators.Any())
            {
                return await next(cancellationToken);
            }

            var context = new ValidationContext<TRequest>(request);

            var validationFailures = (await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (!validationFailures.Any())
            {
                return await next(cancellationToken);
            }

            var errors = validationFailures
                .Select(f => new MbError(f.ErrorCode, f.ErrorMessage))
                .ToList();

            var responseType = typeof(TResponse);

            //Обработка случай для обобщенного MbResult<T>
            if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(MbResult<>))
            {
                var genericArgument = responseType.GetGenericArguments()[0];
                var resultType = typeof(MbResult<>).MakeGenericType(genericArgument);
                var failureMethod = resultType.GetMethod(nameof(MbResult<object>.Failure), [typeof(IEnumerable<MbError>)
                ]);

                if (failureMethod == null)
                {
                    throw new InvalidOperationException($"Could not find the static method '{nameof(MbResult<object>.Failure)}' on generic type '{responseType.Name}'.");
                }

                return (TResponse)failureMethod.Invoke(null, [errors]);
            }

            // Обработка случая для необобщенного MbResult
            // ReSharper disable once InvertIf Решарпер предлогает непонятное для меня решение
            if (responseType == typeof(MbResult))
            {
                var failureMethod = typeof(MbResult).GetMethod(nameof(MbResult.Failure), [typeof(IEnumerable<MbError>)]);

                if (failureMethod == null)
                {
                    throw new InvalidOperationException($"Could not find the static method '{nameof(MbResult.Failure)}' on type '{nameof(MbResult)}'.");
                }

                return (TResponse)failureMethod.Invoke(null, [errors]);
            }

            throw new InvalidOperationException($"Cannot create a failure result for type {responseType.Name}. Ensure all handlers with validation return an MbResult or MbResult<T>.");
        }
    }
}
