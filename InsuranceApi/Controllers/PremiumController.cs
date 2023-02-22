using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PremiumController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> CalculatePremium(
        [FromQuery] string name,
        [FromQuery] DateTime dateOfBirth,
        [FromQuery] string occupation,
        [FromQuery] decimal sumInsured)
    {
        return Ok();
    }

}
