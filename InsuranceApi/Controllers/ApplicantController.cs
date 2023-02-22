using InsuranceApi.Domain.ApplicantConfiguration.Models;
using InsuranceApi.Domain.ApplicantConfiguration.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicantController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApplicantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Config")]
    public async Task<IActionResult> GetApplicantConfig()
    {
        GetApplicantConfigurationQuery query = new();

        ApplicantConfiguration? applicantConfig = (ApplicantConfiguration?)await _mediator.Send(query);

        return Ok(applicantConfig);
    }
}