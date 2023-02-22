using InsuranceApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Tests.Controllers;

public class PremiumControllerTests
{
    [Fact]
    public async Task Get_OnSuccess_Returns200StatusCode()
    {
        PremiumController controller = new();

        var result = (ObjectResult) await controller.CalculatePremium();

        result.StatusCode.Should().Be(200);
    }
}
