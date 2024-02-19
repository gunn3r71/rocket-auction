namespace RocketAuction.Api.Domain.Aggregates.Base
{
    public interface IRepository<T> where T : IAggregateRoot
    {
    }
}