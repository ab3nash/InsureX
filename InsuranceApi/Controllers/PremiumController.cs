using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PremiumController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> CalculatePremium()
    {
        return Ok();
    }

}
