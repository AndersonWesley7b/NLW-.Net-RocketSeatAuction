using Microsoft.AspNetCore.Mvc;
using RocketSeatAuction.API.UseCases.Auctions.GetCurrent;

namespace RocketSeatAuction.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuctionController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCurrentAuction()
    {
        var UseCase = new GetCurrentAuctionUseCase();

        var Result = UseCase.Execute();    

        return Ok(Result);
    }
    

}

