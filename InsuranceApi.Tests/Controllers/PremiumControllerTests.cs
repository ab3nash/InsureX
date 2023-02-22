using InsuranceApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Tests.Controllers;

public class PremiumControllerTests
{
    private readonly string _validName= "Jon Doe";
    private readonly DateTime _validDateOfBirth =  DateTime.UtcNow;
    private readonly string _validOccupation = "Cleaner";
    private readonly decimal _validSumInsured = 1000;

    [Fact]
    public async Task CalculatePremium_OnSuccess_Returns200StatusCode()
    {
        PremiumController controller = new();

        var result = (StatusCodeResult)await controller.CalculatePremium(_validName, _validDateOfBirth, _validOccupation, _validSumInsured);

        result.StatusCode.Should().Be(200);
    }
}
