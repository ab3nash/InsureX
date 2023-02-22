namespace InsuranceApi.Domain.ApplicantConfiguration.Models;

public record ApplicantConfiguration(int MaxAge, List<string>? Occupations);