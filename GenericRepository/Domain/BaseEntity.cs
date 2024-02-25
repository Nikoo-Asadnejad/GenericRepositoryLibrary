using GenericRepository.Abstractions;

namespace GenericRepository.Entities;

public abstract class BaseEntity 
{
    public long CreateDate { get; private set; }
    public long? UpdateDate { get; private set; }
    public long? DeleteDate { get; private set; }
    public bool IsDeleted { get; private set; } = false;

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private List<IDomainEvent> _domainEvents = new();
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public BaseEntity Create()
    {
        this.CreateDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        return this;
    }
    public BaseEntity Update()
    {
        if (CreateDate is 0)
            throw new Exception("Entity State is not valid for updating");
        
        this.UpdateDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        return this;
    }
    public BaseEntity Delete()
    {
        if (CreateDate is 0)
            throw new Exception("Entity State is not valid for deleting");
        
        this.DeleteDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        this.IsDeleted = true;
        return this;
    }


}