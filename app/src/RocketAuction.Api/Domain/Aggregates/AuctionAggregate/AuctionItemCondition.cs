using System.ComponentModel.DataAnnotations;

namespace RocketAuction.Api.Domain.Aggregates.AuctionAggregate;

public enum AuctionItemCondition
{
    [Display(Name = "As If It Were New")]
    AsIfItWereNew,
    New,
    Used
}