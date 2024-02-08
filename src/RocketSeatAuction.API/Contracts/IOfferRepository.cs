using RocketSeatAuction.API.Entities;

namespace RocketSeatAuction.API.Contracts;

public interface IOfferRepository
{
    void Add(Offer Offer);
}
