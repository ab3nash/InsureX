using InsuranceApi.Domain.PremiumCalculation.Models;
using MediatR;

namespace InsuranceApi.Domain.PremiumCalculation.Queries;

public record CalculatePremiumQuery(
    string Name,
    DateTime DateOfBirth, 
    string Occupation,
    decimal SumInsured) : IRequest<CalculatedPremium>;