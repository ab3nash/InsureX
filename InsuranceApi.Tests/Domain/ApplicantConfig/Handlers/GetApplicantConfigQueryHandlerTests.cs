using InsuranceApi.Domain.ApplicantConfiguration.Handlers;
using InsuranceApi.Domain.ApplicantConfiguration.Models;
using InsuranceApi.Domain.ApplicantConfiguration.Queries;
using InsuranceApi.Exceptions;
using Microsoft.Extensions.Options;

namespace InsuranceApi.Tests.Domain.ApplicantConfig.Handlers;
public class GetApplicantConfigQueryHandlerTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task Handle_Returns_ApplicantConfiguration()
    {
        IOptions<InsuranceApi.Domain.Common.Models.ApplicantConfig> applicantConfigOptions = Options.Create(
           new InsuranceApi.Domain.Common.Models.ApplicantConfig {
               MaxAge = 21,
               Occupations = new List<string> { "Clerk", "Doctor" }
           });
        GetApplicantConfigQueryHandler handler = new(applicantConfigOptions);
        GetApplicantConfigurationQuery query = _fixture.Create<GetApplicantConfigurationQuery>();

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeOfType<ApplicantConfiguration>();
    }


    [Fact]
    public async Task Handle_MissingOccupations_Throws_InvalidConfigException()
    {
        IOptions<InsuranceApi.Domain.Common.Models.ApplicantConfig> applicantConfigOptions = Options.Create(
           new InsuranceApi.Domain.Common.Models.ApplicantConfig {
               MaxAge = 21,
               Occupations = new List<string>()
           });
        GetApplicantConfigQueryHandler handler = new(applicantConfigOptions);
        GetApplicantConfigurationQuery query = _fixture.Create<GetApplicantConfigurationQuery>();

        await Assert.ThrowsAsync<InvalidConfigException>(() => handler.Handle(query, CancellationToken.None));
    }
}