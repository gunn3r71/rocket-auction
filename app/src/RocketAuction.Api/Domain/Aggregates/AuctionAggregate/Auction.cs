using RocketAuction.Api.Domain.Aggregates.Base;
using RocketAuction.Api.Domain.Validations;

namespace RocketAuction.Api.Domain.Aggregates.AuctionAggregate
{
    public sealed class Auction : Entity, IAggregateRoot
    {
        public Auction(string name,
                       DateTime startsAt,
                       DateTime endsAt,
                       IValidationContext validationContext)
        {
            Name = name;
            StartsAt = startsAt;
            EndsAt = endsAt;
            
            Validate(validationContext);
        }

        public string Name { get; private set; }
        public DateTime StartsAt { get; private set; }
        public DateTime EndsAt { get; private set; }
        public IList<AuctionItem> Items { get; } = new List<AuctionItem>();

        public void AddItem(AuctionItem item)
        {
            if (Items.Contains(item))
                throw new InvalidOperationException("Auction item already exists");
            
            Items.Add(item);
        }

        public void RemoveItem(AuctionItem item)
        {
            var auctionItem = Items.SingleOrDefault(x => x.Id.Equals(item.Id));

            auctionItem?.UnlinkFromAuction();
        }
        
        public override void Validate(IValidationContext context)
        {
            if (Name is { Length: < 3 or > 60})
                context.AddNotification(nameof(Name), "The auction name must be longer than 3 to 60 characters.");

            if (StartsAt >= EndsAt)
                context.AddNotification("Auction dates", "The auction start and end dates are invalid.");
        }
    }
}
