using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutWise.Domain.Common.Entities;

public abstract class Entity<TId> : ISoftDeletable
{
    public TId Id { get; protected set; } = default!;
    public DateTime CreateDate { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; private set; }
    public bool IsDeleted { get; set; } = false;

    protected Entity() { }

    protected Entity(TId id)
    {
        Id = id;
    }

    public void UpdateTimestamp()
    {
        UpdatedDate = DateTime.UtcNow;
    }
}

