namespace InsuranceApi.Services;

public interface IOccupationRatingService
{
    decimal GetRatingFactor(string occupation);
}
