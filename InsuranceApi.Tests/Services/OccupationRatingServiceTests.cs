using InsuranceApi.Domain.Common.Models;
using InsuranceApi.Exceptions;
using InsuranceApi.Services;
using Microsoft.Extensions.Options;

namespace InsuranceApi.Tests.Services;
public class OccupationRatingServiceTests
{

    [Fact]
    public void GetRatingFactor_ShouldReturn_NonZeroFactor()
    {
        OccupationRatingService occupationRatingService = GetValidOccupationRatingService();

        var ratingFactor = occupationRatingService.GetRatingFactor("O1");

        ratingFactor.Should().BePositive();
    }

    [Theory]
    [InlineData("O1", 1)]
    [InlineData("O2", 1)]
    [InlineData("O3", 1.5)]
    public void GetRatingFactor_ShouldReturn_CorrectFactor(string occupation, decimal factor)
    {
        OccupationRatingService occupationRatingService = GetValidOccupationRatingService();

        var ratingFactor = occupationRatingService.GetRatingFactor(occupation);

        ratingFactor.Should().Be(factor);
    }

    [Fact]
    public void GetRatingFactor_InvalidOccupation_ShouldThrow_NotSupportedException()
    {
        OccupationRatingService occupationRatingService = GetValidOccupationRatingService();

        Assert.Throws<NotSupportedException>(() => occupationRatingService.GetRatingFactor("O5"));
    }

    [Fact]
    public void GetRatingFactor_OccupationRatingConfigEmpty_Throws_InvalidConfigException()
    {
        IOptions<List<OccupationRating>> occupationRatingsOptions = Options.Create(new List<OccupationRating>());

        IOptions<List<RatingFactor>> ratingFactorsOptions = Options.Create(new List<RatingFactor>
        {
            new RatingFactor {
                Rating = "A",
                Factor = 1
            },
            new RatingFactor {
                Rating = "B",
                Factor = 1.5m
            }
        });

        OccupationRatingService occupationRatingService = new(occupationRatingsOptions, ratingFactorsOptions);

        Assert.Throws<InvalidConfigException>(() => occupationRatingService.GetRatingFactor("O1"));
    }

    [Fact]
    public void GetRatingFactor_RatingFactorConfigEmpty_Throws_InvalidConfigException()
    {
        IOptions<List<OccupationRating>> occupationRatingsOptions = Options.Create(new List<OccupationRating>
        {
            new OccupationRating {
                Name = "O1",
                Rating = "A"
            },
            new OccupationRating {
                Name = "O2",
                Rating = "A"
            },
            new OccupationRating {
                Name = "O3",
                Rating = "B"
            }
        });

        IOptions<List<RatingFactor>> ratingFactorsOptions = Options.Create(new List<RatingFactor>());

        OccupationRatingService occupationRatingService = new(occupationRatingsOptions, ratingFactorsOptions);

        Assert.Throws<InvalidConfigException>(() => occupationRatingService.GetRatingFactor("O1"));
    }

    [Fact]
    public void GetRatingFactor_RatingFactorConfigMissing_Throws_InvalidConfigException()
    {
        IOptions<List<OccupationRating>> occupationRatingsOptions = Options.Create(new List<OccupationRating>
        {
            new OccupationRating {
                Name = "O1",
                Rating = "A"
            },
            new OccupationRating {
                Name = "O2",
                Rating = "A"
            },
            new OccupationRating {
                Name = "O3",
                Rating = "B"
            }
        });

        IOptions<List<RatingFactor>> ratingFactorsOptions = Options.Create(new List<RatingFactor>
        {
            new RatingFactor {
                Rating = "A",
                Factor = 1
            }
        });

        OccupationRatingService occupationRatingService = new(occupationRatingsOptions, ratingFactorsOptions);

        Assert.Throws<InvalidConfigException>(() => occupationRatingService.GetRatingFactor("O3"));
    }

    private static OccupationRatingService GetValidOccupationRatingService()
    {
        IOptions<List<OccupationRating>> occupationRatingsOptions = Options.Create(new List<OccupationRating>
        {
            new OccupationRating {
                Name = "O1",
                Rating = "A"
            },
            new OccupationRating {
                Name = "O2",
                Rating = "A"
            },
            new OccupationRating {
                Name = "O3",
                Rating = "B"
            }
        });

        IOptions<List<RatingFactor>> ratingFactorsOptions = Options.Create(new List<RatingFactor>
        {
            new RatingFactor {
                Rating = "A",
                Factor = 1
            },
            new RatingFactor {
                Rating = "B",
                Factor = 1.5m
            }
        });

        return new(occupationRatingsOptions, ratingFactorsOptions);
    }
}