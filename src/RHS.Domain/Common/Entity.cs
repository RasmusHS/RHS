namespace RHS.Domain.Common;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; }
    public DateTime LastModified { get; protected set; }
    public DateTime Created { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// Entities have both referential equality as well as identifier equality
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
        // var other = obj as Entity;
        // if (ReferenceEquals(other, null))
        // {
        //     return false;
        // }
        // if (ReferenceEquals(this, other))
        // {
        //     return true;
        // }
        // if (GetType() != other.GetType())
        // {
        //     return false;
        // }
        // if (Id == 0 || other.Id == 0) //has not been set yet, thus they cannot be equal
        // {
        //     return false;
        // }
        // return Id == other.Id; //identifier equality            
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }
    
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    // public static bool operator ==(Entity a, Entity b)
    // {
    //     if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
    //     {
    //         return true;
    //     }
    //     if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
    //     {
    //         return false;
    //     }
    //     return a.Equals(b);
    // }
    //
    // public static bool operator !=(Entity a, Entity b)
    // {
    //     return !(a == b);
    // }
    //
    // public override int GetHashCode()
    // {
    //     return (GetType().ToString() + Id).GetHashCode();
    // }
}
