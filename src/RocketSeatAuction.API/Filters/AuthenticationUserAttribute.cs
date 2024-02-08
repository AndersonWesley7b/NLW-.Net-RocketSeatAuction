using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketSeatAuction.API.Contracts;

namespace RocketSeatAuction.API.Filters;

public class AuthenticationUserAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly IUserRepository _repository;
    public AuthenticationUserAttribute(IUserRepository repository) => _repository = repository;

    public void OnAuthorization(AuthorizationFilterContext Context)
    {
        try
        {
            var Token = TokenOnRequest(Context.HttpContext);

            var Email = FromBase64String(Token);

            var Exist = _repository.ExistsUserWithEmail(Email);

            if (Exist == false)
            {
                Context.Result = new UnauthorizedObjectResult("Email not valid");
            }

        }
        catch (Exception ex)
        {
            Context.Result = new UnauthorizedObjectResult(ex.Message);
        }
       

        
    }

    private string FromBase64String(string Base64)
    {
        var Data = Convert.FromBase64String(Base64);
        return System.Text.Encoding.UTF8.GetString(Data);
    }

    private string TokenOnRequest(HttpContext Context)
    {
        var Authentication = Context. Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(Authentication))
            throw new Exception("Token is missing");

        return Authentication["Bearer ".Length..];

    }
}
