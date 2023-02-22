using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using InsuranceApi.Domain.PremiumCalculation.Models;
using InsuranceApi.Domain.PremiumCalculation.Queries;
using InsuranceApi.Helpers;
using MediatR;

namespace InsuranceApi.Tests.Helpers;
public class ValidationBehaviorTests
{
    private readonly Mock<IValidator<CalculatePremiumQuery>> _validator = new();
    private readonly Mock<RequestHandlerDelegate<CalculatedPremium>> _requestHandlerDelegate = new();
    private readonly ValidationBehavior<CalculatePremiumQuery, CalculatedPremium> _validationBehavior;
    private readonly Fixture _fixture = new();

    public ValidationBehaviorTests()
    {
        _validationBehavior = new(new List<IValidator<CalculatePremiumQuery>> {
            _validator.Object
        });
    }

    [Fact]
    public async Task Handle_ShouldThrow_ValidationException_IfValidationErrorsExist()
    {
        CalculatePremiumQuery query = _fixture.Create<CalculatePremiumQuery>();

        _validator.Setup(v => v.ValidateAsync(
            It.IsAny<ValidationContext<CalculatePremiumQuery>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult {
                Errors = new List<ValidationFailure>
                {
                    new(nameof(CalculatePremiumQuery.Name), "Invalid Name")
                }
            });

        await Assert.ThrowsAsync<ValidationException>(
            () => _validationBehavior.Handle(query, _requestHandlerDelegate.Object, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldNotThrow_ValidationException_IfValidationErrorDoesNotExist()
    {
        CalculatePremiumQuery query = _fixture.Create<CalculatePremiumQuery>();

        _validator.Setup(v => v.ValidateAsync(
            It.IsAny<ValidationContext<CalculatePremiumQuery>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        await _validationBehavior.Handle(query, _requestHandlerDelegate.Object, CancellationToken.None);

        _requestHandlerDelegate.Verify(d => d());
    }

    [Fact]
    public async Task Handle_ShouldNotThrow_ValidationException_IfValidatorDoesNotExist()
    {
        ValidationBehavior<CalculatePremiumQuery, CalculatedPremium> validationBehavior =
            new(new List<IValidator<CalculatePremiumQuery>>());

        CalculatePremiumQuery query = _fixture.Create<CalculatePremiumQuery>();

        await validationBehavior.Handle(query, _requestHandlerDelegate.Object, CancellationToken.None);

        _requestHandlerDelegate.Verify(d => d());
    }
}
