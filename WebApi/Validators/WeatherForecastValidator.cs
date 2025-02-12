using FluentValidation;
using WebApi.Domain;

namespace WebApi.Validators
{
    public class WeatherForecastValidator : AbstractValidator<WeatherForecast>
    {
        public WeatherForecastValidator()
        {
            RuleFor(x => x.Summary).Length(2,4).WithMessage("طول رشته خلاصه بین 2 و 4 می باشد");

            RuleFor(x => x.TemperatureC).GreaterThan(15).WithMessage("مقدار درجه سانتیگراد باید بزرگتر از 15 باشد");
        }

    }
}