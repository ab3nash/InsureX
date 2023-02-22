using InsuranceApi.Controllers;
using InsuranceApi.Domain.ApplicantConfiguration.Models;
using InsuranceApi.Domain.ApplicantConfiguration.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Tests.Controllers;

public class ApplicantControllerTests
{
    private readonly Mock<IMediator> _mediator = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task GetApplicantConfig_OnSuccess_Returns200StatusCode()
    {
        ApplicantController controller = new(_mediator.Object);

        var result = (OkObjectResult)await controller.GetApplicantConfig();

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetApplicantConfig_Invokes_Mediator()
    {
        ApplicantController controller = new(_mediator.Object);
        GetApplicantConfigurationQuery query = _fixture.Create<GetApplicantConfigurationQuery>();

        _mediator
            .Setup(m => m.Send(It.IsAny<GetApplicantConfigurationQuery>(), It.IsAny<CancellationToken>()));

        await controller.GetApplicantConfig();

        _mediator.Verify(m => m.Send(query, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetApplicantConfig_OnSuccess_Returns_ApplicantConfigResponse()
    {
        ApplicantController controller = new(_mediator.Object);
        GetApplicantConfigurationQuery query = _fixture.Create<GetApplicantConfigurationQuery>();
        ApplicantConfiguration configResponse = _fixture.Create<ApplicantConfiguration>();

        _mediator
            .Setup(m => m.Send(It.IsAny<GetApplicantConfigurationQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(configResponse);

        var response = (OkObjectResult)await controller.GetApplicantConfig();

        response.Value.Should().BeOfType<ApplicantConfiguration>();
    }
}