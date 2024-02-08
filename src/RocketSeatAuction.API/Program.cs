using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RocketSeatAuction.API.Contracts;
using RocketSeatAuction.API.Filters;
using RocketSeatAuction.API.Repositories;
using RocketSeatAuction.API.Repositories.DataAccess;
using RocketSeatAuction.API.Services;
using RocketSeatAuction.API.UseCases.Auctions.GetCurrent;
using RocketSeatAuction.API.UseCases.Offers.CreateOffer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        },
    }) ;
});

builder.Services.AddScoped<AuthenticationUserAttribute>();

builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();

builder.Services.AddScoped<IOfferRepository, OfferRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<LoggedUser>();

builder.Services.AddScoped<CreateOfferUseCase>();

builder.Services.AddScoped<GetCurrentAuctionUseCase>();

builder.Services.AddDbContext<RocketSeatAuctionDbContext>(options =>
{
    options.UseSqlite("Data Source=C:\\Users\\ander\\Downloads\\leilaoDbNLW.db");
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
