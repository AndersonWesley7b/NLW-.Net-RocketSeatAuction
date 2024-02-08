using Bogus;
using FluentAssertions;
using Moq;
using RocketSeatAuction.API.Contracts;
using RocketSeatAuction.API.Entities;
using RocketSeatAuction.API.Enums;
using RocketSeatAuction.API.UseCases.Auctions.GetCurrent;
using Xunit;

namespace UseCases.Test.Auctions.GetCurrent;
public class GetCurrentAuctionUseCaseTest
{
    [Fact]
    public void Sucess()
    {
        var Entity = new Faker<Auction>()
            .RuleFor(auction => auction.Id, f => f.Random.Number(1, 700))
            .RuleFor(auction => auction.Name, f => f.Lorem.Word())
            .RuleFor(auction => auction.Starts, f => f.Date.Past())
            .RuleFor(auction => auction.Ends, f => f.Date.Future())
            .RuleFor(auction => auction.Items, (f, auction) => new List<Item> { 
                new Item
                {
                    Id = f.Random.Number(1, 700),
                    Name = f.Commerce.ProductName(),
                    Brand = f.Commerce.Department(),
                    BasePrice = f.Random.Decimal(50,1000),
                    Condition = f.PickRandom<Condition>(),
                    AuctionId = auction.Id,
                } 
            }).Generate();


        var Mock = new Mock<IAuctionRepository>();
        Mock.Setup(i => i.GetCurrent()).Returns(Entity);

        var UseCase = new GetCurrentAuctionUseCase(Mock.Object);

        var ReturnedAuction = UseCase.Execute();

        ReturnedAuction.Should().NotBeNull();
        ReturnedAuction.Id.Should().Be(Entity.Id);
        ReturnedAuction.Name.Should().Be(Entity.Name);
    }
}
