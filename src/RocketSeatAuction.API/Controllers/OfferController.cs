using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RocketSeatAuction.API.Communication.Requests;
using RocketSeatAuction.API.Filters;
using RocketSeatAuction.API.UseCases.Offers.CreateOffer;

namespace RocketSeatAuction.API.Controllers;

[ServiceFilter(typeof(AuthenticationUserAttribute))]
public class OfferController : RocketSeatAuctionBaseController
{
    [HttpPost("{ItemId}")]

    public IActionResult CreateOffer([FromRoute]int ItemId,
        [FromBody]RequestCreateOfferJson Request,
        [FromServices] CreateOfferUseCase UseCase)
    {
        int OfferId = UseCase.Execute(ItemId, Request);

        return Created(string.Empty, OfferId);

    }
}
