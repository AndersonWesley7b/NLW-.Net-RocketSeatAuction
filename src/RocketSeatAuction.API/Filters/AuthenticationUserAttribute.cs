using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketSeatAuction.API.Repositories;

namespace RocketSeatAuction.API.Filters;

public class AuthenticationUserAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext Context)
    {
        try
        {
            var Token = TokenOnRequest(Context.HttpContext);

            var Repository = new RocketSeatAuctionDbContext();

            var Email = FromBase64String(Token);

            var Exist = Repository.Users.Any(user => user.Email.Equals(Email));

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
