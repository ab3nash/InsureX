using InsuranceApi.Domain.Common.Models;
using InsuranceApi.Exceptions;
using Microsoft.Extensions.Options;

namespace InsuranceApi.Services;

public class OccupationRatingService : IOccupationRatingService
{
    private readonly List<OccupationRating> _occupationRatings;
    private readonly List<RatingFactor> _ratingFactors;

    public OccupationRatingService(
        IOptions<List<OccupationRating>> occupationRatingConfig,
        IOptions<List<RatingFactor>> ratingFactorConfig)
    {
        _occupationRatings = occupationRatingConfig.Value;
        _ratingFactors = ratingFactorConfig.Value;
    }

    public decimal GetRatingFactor(string occupation)
    {
        if(_occupationRatings!.Any() != true)
        { 
            throw new InvalidConfigException("OccupationRatingConfig");
        }

        if(_ratingFactors!.Any() != true)
        { 
            throw new InvalidConfigException("RatingFactorConfig");
        }

        string? rating = _occupationRatings!.FirstOrDefault(or => or.Name == occupation)?.Rating;

        if(string.IsNullOrEmpty(rating))
        {
            throw new NotSupportedException($"Occupation '{occupation}' is not supported");
        }

        decimal? ratingFactor = _ratingFactors!.FirstOrDefault(rf => rf.Rating == rating)?.Factor;

        if(ratingFactor == null)
        {
            throw new InvalidConfigException("RatingFactorConfig");
        }

        return ratingFactor.Value;
    } 
}
