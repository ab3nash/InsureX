using FluentValidation;
using InsuranceApi.Domain.Common.Models;
using InsuranceApi.Domain.PremiumCalculation.Queries;
using InsuranceApi.Domain.PremiumCalculation.Validators;
using InsuranceApi.Helpers;
using InsuranceApi.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
RegisterServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


void RegisterServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddOptions<ApplicantConfig>().Bind(configuration.GetSection("ApplicantConfig"));
    services.AddOptions<List<OccupationRating>>().Bind(configuration.GetSection("OccupationRatingConfig"));
    services.AddOptions<List<RatingFactor>>().Bind(configuration.GetSection("RatingFactorConfig"));

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    services.AddTransient<IValidator<CalculatePremiumQuery>, CalculatePremiumQueryValidator>();
    services.AddScoped<IOccupationRatingService, OccupationRatingService>();
}