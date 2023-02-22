using FluentValidation.TestHelper;
using InsuranceApi.Domain.Common.Models;
using InsuranceApi.Domain.PremiumCalculation.Queries;
using InsuranceApi.Domain.PremiumCalculation.Validators;
using Microsoft.Extensions.Options;

namespace InsuranceApi.Tests.Domain.PremiumCalculation.Validators;
public class CalculatePremiumQueryValidatorTests
{
    private readonly CalculatePremiumQueryValidator _validator;

    public CalculatePremiumQueryValidatorTests()
    {
        IOptions<ApplicantConfig> applicantConfigOptions = Options.Create(new ApplicantConfig {
            MaxAge = 21,
            Occupations = new List<string> { "Clerk", "Doctor" }
        });
        _validator = new CalculatePremiumQueryValidator(applicantConfigOptions);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Name_ShouldNotBeEmpty(string name)
    {
        CalculatePremiumQuery query = new(name, DateTime.UtcNow.AddYears(-10), "Clerk", 2010);

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.Name);
    }

    [Theory]
    [InlineData(40)]
    [InlineData(22)]
    public void Age_ShouldNotExceed_MaxAge(int age)
    {
        CalculatePremiumQuery query = new("Jon Doe", DateTime.UtcNow.AddYears(-age), "Clerk", 2010);

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.DateOfBirth);
    }

    [Theory]
    [InlineData("PM")]
    [InlineData("President")]
    public void Occupation_ShouldBeIn_OccupationsList(string occupation)
    {
        CalculatePremiumQuery query = new("Jon Doe", DateTime.UtcNow.AddYears(-10), occupation, 2010);

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.Occupation);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void SumInsured_ShouldBe_Positive(int sumInsured)
    {
        CalculatePremiumQuery query = new("Jon Doe", DateTime.UtcNow.AddYears(-10), "Clerk", sumInsured);

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.SumInsured);
    }

    [Fact]
    public void ValidQuery_ShouldNotHave_ValidationErrors()
    {
        CalculatePremiumQuery query = new("Jon Doe", DateTime.UtcNow.AddYears(-10), "Clerk", 2000);

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveAnyValidationErrors();
    }
}