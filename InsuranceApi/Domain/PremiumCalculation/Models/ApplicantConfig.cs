namespace InsuranceApi.Domain.PremiumCalculation.Models;

public class ApplicantConfig
{
    public int MaxAge { get; set; }
    public List<Occupation>? Occupations { get; set; }
}