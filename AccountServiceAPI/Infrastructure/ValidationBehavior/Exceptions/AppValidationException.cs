using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountServiceAPI.Infrastructure.ValidationBehavior.Exceptions
{
    public class AppValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public AppValidationException(IEnumerable<ValidationFailure> failures)
            : base("Произошла одна или несколько ошибок валидации.")
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
