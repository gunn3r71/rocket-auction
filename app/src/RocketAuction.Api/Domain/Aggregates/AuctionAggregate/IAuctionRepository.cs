using RocketAuction.Api.Domain.Aggregates.Base;

namespace RocketAuction.Api.Domain.Aggregates.AuctionAggregate;

public interface IAuctionRepository : IRepository<Auction>
{
    Task<Guid> AddAsync(Auction auction);
    Task UpdateAsync(Auction auction);
    Task<IEnumerable<Auction>> GetAuctionsAsync(bool loadItems = false);
    Task<Auction> GetAuctionByIdAsync(Guid id, bool loadItems = false);
    Task<IEnumerable<AuctionItem>> GetAuctionItemsByAuctionId(Guid auctionId);
}