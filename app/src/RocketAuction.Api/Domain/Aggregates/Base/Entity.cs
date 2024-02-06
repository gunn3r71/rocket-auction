using System.Diagnostics.CodeAnalysis;
using RocketAuction.Api.Domain.Validations;

namespace RocketAuction.Api.Domain.Aggregates.Base
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        protected Entity(Guid id)
        {
            Id = !id.Equals(Guid.Empty) ? id : throw new ArgumentOutOfRangeException(nameof(id), "O identificador da entidade não deve ser nulo.");
        }

        public Guid Id { get; private set; }

        public abstract void Validate(IValidationContext context);
    }
}
