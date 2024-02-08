using RocketSeatAuction.API.Entities;

namespace RocketSeatAuction.API.Contracts;

public interface IAuctionRepository
{
    Auction? GetCurrent();
}
