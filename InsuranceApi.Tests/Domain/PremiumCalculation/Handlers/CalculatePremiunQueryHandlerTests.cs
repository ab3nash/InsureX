using InsuranceApi.Domain.PremiumCalculation.Handlers;
using InsuranceApi.Domain.PremiumCalculation.Models;
using InsuranceApi.Domain.PremiumCalculation.Queries;
using InsuranceApi.Services;

namespace InsuranceApi.Tests.Domain.PremiumCalculation.Handlers;
public class CalculatePremiunQueryHandlerTests
{
    private readonly CalculatePremiumQueryHandler _handler;
    private readonly Fixture _fixture = new();
    private readonly Mock<IOccupationRatingService> _occupationRatingServiceMock = new();

    public CalculatePremiunQueryHandlerTests()
    {
        _occupationRatingServiceMock.Setup(o => o.GetRatingFactor("Doctor")).Returns(1);
        _occupationRatingServiceMock.Setup(o => o.GetRatingFactor("Nurse")).Returns(1.5m);
        _handler = new(_occupationRatingServiceMock.Object);
    }

    [Fact]
    public async Task Handle_Returns_CalculatedPremium()
    {
        CalculatePremiumQuery query = _fixture.Create<CalculatePremiumQuery>();

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeOfType<CalculatedPremium>();
    }

    [Theory]
    [InlineData(31, "Doctor", 0, 0, 0)]
    [InlineData(20, "Doctor", 12340, 2961.6, 200)]
    [InlineData(55, "Nurse", 2468, 2443.32, 165)]
    [InlineData(33, "Nurse", 4319, 2565.486, 173.25)]
    [InlineData(0, "Nurse", 4319, 0, 0)]
    public async Task Handle_Returns_CorrectPremiums(
        int age,
        string occcupation,
        decimal sumInsured,
        decimal monthlyDeathPremium,
        decimal monthlyTpdPremium)
    {
        CalculatePremiumQuery query = new(
            Name: "Test",
            DateOfBirth: DateTime.UtcNow.AddYears(-age),
            occcupation,
            sumInsured);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.MonthlyDeathPremium.Should().Be(monthlyDeathPremium);
        result.MonthlyTpdPremium.Should().Be(monthlyTpdPremium);
    }
}