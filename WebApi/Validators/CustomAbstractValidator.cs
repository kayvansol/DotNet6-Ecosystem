using FluentValidation;
using FluentValidation.Results;

namespace WebApi.Validators
{
    public class CustomAbstractValidator<TRequest> : AbstractValidator<TRequest>
    {
        
        protected override void RaiseValidationException(ValidationContext<TRequest> _context, ValidationResult result)
        {

            /*var firstError = result.Errors[0];

            var ex = new HttpExcpetion(firstError.ErrorMessage, (int)HttpStatusCode.BadRequest);

            throw ex;*/

            var validator = result.Errors;

            if (validator.Any())
            {
                
                var failures = validator.Where(v => v.ErrorMessage.Any()).
                    SelectMany(r => r.ErrorMessage).ToList();

                if (failures.Any())
                {
                    throw new ValidationException(failures.ToString());
                }
            }

        }
    }
}
