using FluentAssertions;
using RocketAuction.Api.Domain.Aggregates.AuctionAggregate;
using RocketAuction.Api.Domain.Validations;

namespace RocketAuction.Domain.UnitTests.Aggregates.AuctionAggregates;

public class AuctionItensTests
{
    [Fact(DisplayName = "When the brand is not informed, it must be set as generic")]
    [Trait("Category", "success")]
    public void WhenTheBrandIsntInformedItMustBeSetAsGeneric()
    {
        IValidationContext validationContext = new ValidationContext();
        var item = new AuctionItem(string.Empty, 10M, AuctionItemCondition.New, Guid.Empty, validationContext);

        item.Brand.Should().Be("Generic");
    }

    [Fact(DisplayName = "When creating an auction item with invalid properties, notification must be generated in the validation context")]
    [Trait("Category", "fail")]
    public void When_CreatingAnAuctionItemWithInvalidProperties_NotificationMustBeGeneratedInTheValidationContext()
    {
        IValidationContext validationContext = new ValidationContext();
        var item = new AuctionItem(new string('a', 54), 10M, AuctionItemCondition.New, Guid.Empty, validationContext);

        validationContext.HasErrors().Should().BeTrue();
        validationContext.GetNotifications().Count.Should().Be(1);
    }

    [Fact(DisplayName = "When trying to unlink from auction and is already unlinked, an exception must be thrown")]
    [Trait("Category", "fail")]
    public void When_TryingToUnlinkFromAuctionAndIsAlreadyUnlinked_AnExceptionMustBeThrown()
    {
        IValidationContext validationContext = new ValidationContext();
        var item = new AuctionItem(new string('a', 54), 10M, AuctionItemCondition.New, Guid.Empty, validationContext);

        var auction = () => item.UnlinkFromAuction();

        auction();

        auction.Should().ThrowExactly<InvalidOperationException>();
    }
}