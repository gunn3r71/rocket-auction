using RocketAuction.Api.Domain.Aggregates.Base;
using RocketAuction.Api.Domain.Validations;

namespace RocketAuction.Api.Domain.Aggregates.AuctionAggregate;

public sealed class AuctionItem : Entity
{
    public AuctionItem(string brand,
                       decimal basePrice,
                       AuctionItemCondition condition,
                       Guid auctionId,
                       IValidationContext validationContext)
    {
        Brand = brand;
        BasePrice = basePrice;
        Condition = condition;
        AuctionId = auctionId;
        
        Validate(validationContext);
    }

    public string Brand { get; private set; }
    public decimal BasePrice { get; private set; }
    public AuctionItemCondition Condition { get; private set; }
    public Guid? AuctionId { get; private set; }

    public void UnlinkFromAuction()
    {
        if (AuctionId is null)
            throw new InvalidOperationException("Auction item is already disassociated from auction.");

        AuctionId = null;
    }
    
    public override void Validate(IValidationContext context)
    {
        if (Brand is { Length: 0 })
            Brand = "Generic";

        if (Brand is {Length: > 50})
            context.AddNotification(nameof(Brand), "The brand name cannot have more than 50 characters.");

        if (BasePrice <= 0)
            context.AddNotification(nameof(BasePrice), "Invalid base price.");
    }
}