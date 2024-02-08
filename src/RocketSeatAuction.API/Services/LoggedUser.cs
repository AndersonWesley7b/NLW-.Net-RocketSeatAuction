using RocketSeatAuction.API.Contracts;
using RocketSeatAuction.API.Entities;
using RocketSeatAuction.API.Repositories;

namespace RocketSeatAuction.API.Services;

public class LoggedUser : ILoggedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;
    public LoggedUser(IHttpContextAccessor httpContext, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContext;
        _userRepository = userRepository;
    }
    public User User()
    {
        var Token = TokenOnRequest();

        var Email = FromBase64String(Token);

        return _userRepository.GetUserByEmail(Email);
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
