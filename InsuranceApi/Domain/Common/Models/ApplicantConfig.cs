namespace InsuranceApi.Domain.Common.Models;

public class ApplicantConfig
{
    public int MaxAge { get; set; }
    public List<string>? Occupations { get; set; }
}