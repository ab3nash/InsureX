using InsuranceApi.Domain.PremiumCalculation.Models;
using InsuranceApi.Domain.PremiumCalculation.Queries;
using InsuranceApi.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InsuranceApi.Domain.PremiumCalculation.Handlers;

public class CalculatePremiumQueryHandler : IRequestHandler<CalculatePremiumQuery, CalculatedPremium>
{
    private readonly IOccupationRatingService _occupationRatingService;

    public CalculatePremiumQueryHandler(IOccupationRatingService occupationRatingService)
    {
        _occupationRatingService = occupationRatingService;
    }

    public async Task<CalculatedPremium> Handle(CalculatePremiumQuery query, CancellationToken cancellationToken)
    {
        decimal monthlyDeathPremium = 0;
        decimal monthlyTpdPremium = 0;

        // Monthly Death Premium = (Sum Insured * Occupation Rating Factor * Age) / 1000 * 12
        // Monthly TPD Premium = (Sum Insured * Occupation Rating Factor * Age) / 1234
        await Task.Run(() => {
            int age = DateTime.UtcNow.Year - query.DateOfBirth.Year;
            decimal occupationRatingFactor = _occupationRatingService.GetRatingFactor(query.Occupation);
            decimal premiumDeterminant = query.SumInsured * occupationRatingFactor * age;
            monthlyDeathPremium = premiumDeterminant * 0.012m;
            monthlyTpdPremium = premiumDeterminant / 1234;
        }, cancellationToken);

        return new CalculatedPremium(monthlyDeathPremium, monthlyTpdPremium);
    }
}