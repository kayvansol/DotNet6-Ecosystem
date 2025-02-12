using FluentValidation;
using FluentValidation.Results;

namespace WebApi.Validators
{
    public class ModelValidation<T>
    {
        
        public static async Task ValidateAsync(T model,IValidator<T> _validator)
        {
            ValidationResult result = _validator.Validate(model);

            if (!result.IsValid)
            {

                var validator = result.Errors;

                if (validator.Any())
                {

                    var failures = validator.Where(v => v.ErrorMessage.Any()).
                        Select(r => (r.PropertyName, r.ErrorMessage)).ToList();

                    if (failures.Any())
                    {
                        throw new ValidationException(Newtonsoft.Json.JsonConvert.SerializeObject(failures));
                    }
                }

            }
        }
    }
}
