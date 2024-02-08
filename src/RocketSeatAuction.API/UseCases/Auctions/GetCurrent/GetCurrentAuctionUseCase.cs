using Microsoft.EntityFrameworkCore;
using RocketSeatAuction.API.Contracts;
using RocketSeatAuction.API.Entities;
using RocketSeatAuction.API.Repositories;

namespace RocketSeatAuction.API.UseCases.Auctions.GetCurrent;

public class GetCurrentAuctionUseCase
{
    private readonly IAuctionRepository _repository;
    public GetCurrentAuctionUseCase(IAuctionRepository repository) => _repository = repository;

    public Auction? Execute() => _repository.GetCurrent();

}
