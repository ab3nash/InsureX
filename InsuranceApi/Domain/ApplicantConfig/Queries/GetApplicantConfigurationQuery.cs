using MediatR;

namespace InsuranceApi.Domain.ApplicantConfiguration.Queries;

public record GetApplicantConfigurationQuery() : IRequest<Models.ApplicantConfiguration>;