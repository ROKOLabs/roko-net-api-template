namespace Roko.Template.Application.Internal.Behaviors
{
    using Roko.Template.Blocks.Common.Exceptions;
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this._validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<ValidationResult> validationResults = new();

            foreach (var validator in this._validators)
            {
                var result = await validator.ValidateAsync(request, cancellationToken);

                validationResults.Add(result);
            }

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(failure => failure is not null)
                .GroupBy(failure => failure.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(failure => failure.ErrorMessage).ToArray());

            if (failures.Any())
            {
                throw new ServiceValidationException(failures);
            }

            return await next();
        }
    }
}
