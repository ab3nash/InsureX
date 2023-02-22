using InsuranceApi.Domain.PremiumCalculation.Models;
using InsuranceApi.Domain.PremiumCalculation.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PremiumController : ControllerBase
{
    private readonly IMediator _mediator;

    public PremiumController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> CalculatePremium(
        [FromQuery] string name,
        [FromQuery] DateTime dateOfBirth,
        [FromQuery] string occupation,
        [FromQuery] decimal sumInsured)
    {
        CalculatePremiumQuery query = new(name, dateOfBirth, occupation, sumInsured);

        CalculatedPremium? calculatedPremium = (CalculatedPremium?)await _mediator.Send(query);

        return Ok(calculatedPremium);
    }
}
