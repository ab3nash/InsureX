using InsuranceApi.Domain.ApplicantConfiguration.Queries;
using InsuranceApi.Domain.Common.Models;
using InsuranceApi.Exceptions;
using MediatR;
using Microsoft.Extensions.Options;

namespace InsuranceApi.Domain.ApplicantConfiguration.Handlers;

public class GetApplicantConfigQueryHandler : IRequestHandler<GetApplicantConfigurationQuery, Models.ApplicantConfiguration>
{
    private readonly ApplicantConfig _applicantConfig;
    public GetApplicantConfigQueryHandler(IOptions<ApplicantConfig> applicantConfigOptions)
    {
        _applicantConfig = applicantConfigOptions.Value;
    }

    public Task<Models.ApplicantConfiguration> Handle(GetApplicantConfigurationQuery request, CancellationToken cancellationToken)
    {
        if (_applicantConfig.Occupations?.Any() != true)
        {
            throw new InvalidConfigException("ApplicantConfig");
        }

        return Task.FromResult(new Models.ApplicantConfiguration(_applicantConfig.MaxAge, _applicantConfig.Occupations));
    }
}