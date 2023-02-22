using InsuranceApi.Domain.PremiumCalculation.Models;
using InsuranceApi.Domain.PremiumCalculation.Queries;
using MediatR;

namespace InsuranceApi.Domain.PremiumCalculation.Handlers;

public class CalculatePremiumQueryHandler : IRequestHandler<CalculatePremiumQuery, CalculatedPremium>
{
    public Task<CalculatedPremium> Handle(CalculatePremiumQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
