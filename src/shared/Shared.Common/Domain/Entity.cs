namespace Shared.Common.Domain;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents;

    protected Entity()
    {
        _domainEvents = [];
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    public Guid CreatedBy { get; protected set; }
    public Guid? UpdatedBy { get; protected set; }
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
