using Microsoft.EntityFrameworkCore;
using RocketSeatAuction.API.Contracts;
using RocketSeatAuction.API.Entities;

namespace RocketSeatAuction.API.Repositories.DataAccess;

public class AuctionRepository : IAuctionRepository
{
    private readonly RocketSeatAuctionDbContext _dbContext;
    public AuctionRepository(RocketSeatAuctionDbContext dbContext) => _dbContext = dbContext;

    public Auction? GetCurrent()
    {

        return _dbContext
               .Auctions
               .Include(auction => auction.Items)
               .FirstOrDefault();
    }

}
