using Bogus;
using FluentAssertions;
using Moq;
using RocketSeatAuction.API.Communication.Requests;
using RocketSeatAuction.API.Contracts;
using RocketSeatAuction.API.Entities;
using RocketSeatAuction.API.Services;
using RocketSeatAuction.API.UseCases.Auctions.GetCurrent;
using RocketSeatAuction.API.UseCases.Offers.CreateOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UseCases.Test.Offer.CreateOffer;
public class CreateOfferUserCaseTest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Sucess(int ItemId)
    {
        //ARRANGE
        var Request = new Faker<RequestCreateOfferJson>()
            .RuleFor(request => request.Price, f => f.Random.Decimal(1, 700)).Generate();


        var OfferRepository = new Mock<IOfferRepository>();


        var NewUser = new Mock<ILoggedUser>();
        NewUser.Setup(i => i.User()).Returns(new User());

        var UseCase = new CreateOfferUseCase(NewUser.Object, OfferRepository.Object);

        //ACT
        var act = () => UseCase.Execute(ItemId, Request);

        //ASSERT
        act.Should().NotThrow();

    }
}
