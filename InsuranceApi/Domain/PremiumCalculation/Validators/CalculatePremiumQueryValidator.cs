using FluentValidation;
using InsuranceApi.Domain.Common.Models;
using InsuranceApi.Domain.PremiumCalculation.Queries;
using Microsoft.Extensions.Options;

namespace InsuranceApi.Domain.PremiumCalculation.Validators;

public class CalculatePremiumQueryValidator : AbstractValidator<CalculatePremiumQuery>
{
    private readonly ApplicantConfig _applicantConfig;

    public CalculatePremiumQueryValidator(IOptions<ApplicantConfig> applicantConfigOptions)
    {
        _applicantConfig = applicantConfigOptions.Value;

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now.Date.AddYears(-1).Date)
            .WithMessage($"Minimum age allowed is 1 year")
            .GreaterThan(DateTime.Now.AddYears(-_applicantConfig.MaxAge).Date)
            .WithMessage($"Maximum age allowed is {_applicantConfig.MaxAge} years");
        RuleFor(x => x.Occupation).NotEmpty().Must(BeAValidOccupation).WithMessage("Invalid occupation");
        RuleFor(x => x.SumInsured).GreaterThan(0);
    }

    private bool BeAValidOccupation(string occupation)
    {
        return _applicantConfig.Occupations?.Contains(occupation) ?? false;
    }
}