using FluentAssertions;
using RocketAuction.Api.Domain.Aggregates.AuctionAggregate;
using RocketAuction.Api.Domain.Validations;

namespace RocketAuction.Domain.UnitTests.Aggregates.AuctionAggregates;

public class AuctionTests
{
    [Fact(DisplayName = "When creating an auction with invalid properties, notification must be generated in the validation context")]
    [Trait("Category", "fail")]
    public void When_CreatingAnAuctionWithInvalidProperties_NotificationMustBeGeneratedInTheValidationContext()
    {
        IValidationContext validationContext = new ValidationContext();
        
        var auction = new Auction("in", DateTime.Now, DateTime.Now.AddDays(1), validationContext);

        validationContext.HasErrors().Should().BeTrue();
        validationContext.GetNotifications().Count.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "When adding an AuctionItem and it exists, an exception must be thrown")]
    [Trait("Category", "fail")]
    public void When_AddingAnAuctionItemAndItExists_AnExceptionMustBeThrown()
    {
        IValidationContext context = new ValidationContext();
        var auction = new Auction("auction 1", DateTime.Now, DateTime.Now.AddDays(1), context);
        var auctionItem = new AuctionItem("brand", 0M, AuctionItemCondition.AsIfItWereNew, auction.Id, context);
        auction.AddItem(auctionItem);
        
        var action = () => auction.AddItem(auctionItem);

        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact(DisplayName = "When removing an auction item It must run successfully")]
    [Trait("Category", "success")]
    public void When_RemovingAnAuctionItem_ItMustRunSuccessfully()
    {
        IValidationContext context = new ValidationContext();
        var auction = new Auction("auction 1", DateTime.Now, DateTime.Now.AddDays(1), context);
        var auctionItem = new AuctionItem("brand", 10M, AuctionItemCondition.AsIfItWereNew, auction.Id, context);
        auction.AddItem(auctionItem);

        var action = () => auction.RemoveItem(auctionItem);

        action.Should().NotThrow();
    }

    [Fact(DisplayName = "When creating an auction with an invalid start and end date, must add notification in the validation context")]
    [Trait("Category", "fail")]
    public void When_CreatingAnAuctionWithAnInvalidStartAndEndDate_MustAddNotificationIntheValidationContext()
    {
        IValidationContext context = new ValidationContext();
        var auction = new Auction("auction 1", DateTime.Now, DateTime.Now.AddDays(-1), context);

        context.HasErrors().Should().BeTrue();
        context.GetNotifications().Count.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "When trying to remove an auction item that is already removed, an exception must be thrown")]
    [Trait("Category", "fail")]
    public void When_TryingToRemoveAnAuctionItemThatsAlreadyRemoved_AnExceptionMustBeThrown()
    {
        IValidationContext context = new ValidationContext();
        var auction = new Auction("auction 1", DateTime.Now, DateTime.Now.AddDays(1), context);
        var auctionItem = new AuctionItem("brand", 10M, AuctionItemCondition.AsIfItWereNew, auction.Id, context);
        auction.AddItem(auctionItem);
        auction.RemoveItem(auctionItem);
        
        var action = () => auction.RemoveItem(auctionItem);

        action.Should().ThrowExactly<InvalidOperationException>();
    }
}