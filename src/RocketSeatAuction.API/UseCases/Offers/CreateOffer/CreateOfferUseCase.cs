using RocketSeatAuction.API.Communication.Requests;
using RocketSeatAuction.API.Entities;
using RocketSeatAuction.API.Repositories;
using RocketSeatAuction.API.Services;

namespace RocketSeatAuction.API.UseCases.Offers.CreateOffer;

public class CreateOfferUseCase
{
    private readonly LoggedUser _loggedUser;
    public CreateOfferUseCase(LoggedUser loggedUser) => _loggedUser = loggedUser;
    public int Execute(int _ItemId, RequestCreateOfferJson _Request)
    {
        var Repository = new RocketSeatAuctionDbContext();

        var LoggedUser = _loggedUser.User();

        var Offer = new Offer
        {
            CreatedOn = DateTime.Now,
            ItemId = _ItemId,
            Price = _Request.Price,
            UserId = LoggedUser.Id,
        };

        Repository.Offers.Add(Offer);
        Repository.SaveChanges();

        return Offer.Id;

    }
}
