using InsuranceApi.Controllers;
using InsuranceApi.Domain.PremiumCalculation.Models;
using InsuranceApi.Domain.PremiumCalculation.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Tests.Controllers;

public class PremiumControllerTests
{
    private readonly string _validName = "Jon Doe";
    private readonly DateTime _validDateOfBirth = DateTime.Now;
    private readonly string _validOccupation = "Cleaner";
    private readonly decimal _validSumInsured = 1000;
    private readonly Mock<IMediator> _mediator = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task CalculatePremium_OnSuccess_Returns200StatusCode()
    {
        PremiumController controller = new(_mediator.Object);

        var result = (OkObjectResult)await controller.CalculatePremium(
            _validName, _validDateOfBirth, _validOccupation, _validSumInsured);

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task CalculatePremium_Invokes_Mediator()
    {
        PremiumController controller = new(_mediator.Object);
        CalculatePremiumQuery calculatePremiumQuery = _fixture.Create<CalculatePremiumQuery>();

        _mediator
            .Setup(m => m.Send(It.IsAny<CalculatePremiumQuery>(), It.IsAny<CancellationToken>()));

        await controller.CalculatePremium(
            calculatePremiumQuery.Name,
            calculatePremiumQuery.DateOfBirth,
            calculatePremiumQuery.Occupation,
            calculatePremiumQuery.SumInsured);

        _mediator.Verify(m => m.Send(calculatePremiumQuery, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task CalculatePremium_OnSuccess_Returns_CalculatedPremium()
    {
        PremiumController controller = new(_mediator.Object);
        CalculatePremiumQuery calculatePremiumQuery = _fixture.Create<CalculatePremiumQuery>();
        CalculatedPremium calculatedPremium = _fixture.Create<CalculatedPremium>();

        _mediator
            .Setup(m => m.Send(It.IsAny<CalculatePremiumQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(calculatedPremium);

        var response = (OkObjectResult)await controller.CalculatePremium(
            calculatePremiumQuery.Name,
            calculatePremiumQuery.DateOfBirth,
            calculatePremiumQuery.Occupation,
            calculatePremiumQuery.SumInsured);

        response.Value.Should().BeOfType<CalculatedPremium>();
    }
}