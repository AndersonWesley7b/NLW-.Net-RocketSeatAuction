using Microsoft.AspNetCore.Mvc;
using RocketSeatAuction.API.Entities;
using RocketSeatAuction.API.UseCases.Auctions.GetCurrent;

namespace RocketSeatAuction.API.Controllers;

public class AuctionController : RocketSeatAuctionBaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(Auction), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetCurrentAuction([FromServices] GetCurrentAuctionUseCase UseCase)
    {
        var Result = UseCase.Execute();   
        
        if(Result is null)
            return NoContent();

        return Ok(Result);
    }
}

