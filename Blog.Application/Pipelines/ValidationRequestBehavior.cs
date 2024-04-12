using Blog.Application.Response;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Blog.Application.Pipelines
{
    public class ValidationRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse> where TResponse : ResponseBase
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationRequestBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            var failures = _validators
                            .Select(v => v.Validate(request))
                            .SelectMany(result => result.Errors)
                            .Where(f => f != null)
                            .ToList();

            if (failures.Any())
            {
                var errors = Errors(failures);
                return await Task.FromResult(errors);
            }

            return await next();

        }

        private static TResponse Errors(IEnumerable<ValidationFailure> failures)
        {

            Dictionary<string, string[]> errors = new Dictionary<string, string[]>();

            foreach (var failure in failures)
            {
                if (!errors.ContainsKey(failure.PropertyName))
                {
                    string[] errorsMessages = new string[] { failure.ErrorMessage };

                    errors.Add(failure.PropertyName, errorsMessages);
                }
                else
                {
                    var list = errors[failure.PropertyName].ToList();
                    list.Add(failure.ErrorMessage);
                    errors[failure.PropertyName] = list.ToArray();
                    
                }

            }

            ResponseBase response = new ResponseBase
            {
                Errors = errors,
                Message = "Validation error",
                StatusCode = 400,
                IsValid = false
            };

            return (response as TResponse)!;

        }
    }
}
