using RocketSeatAuction.API.Communication.Requests;
using RocketSeatAuction.API.Contracts;
using RocketSeatAuction.API.Entities;
using RocketSeatAuction.API.Repositories;
using RocketSeatAuction.API.Repositories.DataAccess;
using RocketSeatAuction.API.Services;

namespace RocketSeatAuction.API.UseCases.Offers.CreateOffer;

public class CreateOfferUseCase
{
    private readonly LoggedUser _loggedUser;
    private readonly IOfferRepository _offerRepository;

    public CreateOfferUseCase(LoggedUser loggedUser, IOfferRepository offerRepository)
    {
        _loggedUser = loggedUser;
        _offerRepository = offerRepository;

    }
    public int Execute(int _ItemId, RequestCreateOfferJson _Request)
    {

        var LoggedUser = _loggedUser.User();

        var Offer = new Offer
        {
            CreatedOn = DateTime.Now,
            ItemId = _ItemId,
            Price = _Request.Price,
            UserId = LoggedUser.Id,
        };

        _offerRepository.Add(Offer);

        return Offer.Id;

    }
}
