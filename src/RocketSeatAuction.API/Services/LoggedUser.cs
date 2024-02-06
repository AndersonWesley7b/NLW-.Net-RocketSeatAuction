using RocketSeatAuction.API.Entities;
using RocketSeatAuction.API.Repositories;

namespace RocketSeatAuction.API.Services;

public class LoggedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public LoggedUser(IHttpContextAccessor httpContext)
    {
        _httpContextAccessor = httpContext;   
    }
    public User User()
    {
        var Repository = new RocketSeatAuctionDbContext();

        var Token = TokenOnRequest();

        var Email = FromBase64String(Token);

        return Repository.Users.First(user => user.Email.Equals(Email));
    }

    private string FromBase64String(string Base64)
    {
        var Data = Convert.FromBase64String(Base64);
        return System.Text.Encoding.UTF8.GetString(Data);
    }

    private string TokenOnRequest()
    {
        var Authentication = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(Authentication))
            throw new Exception("Token is missing");

        return Authentication["Bearer ".Length..];

    }
}
