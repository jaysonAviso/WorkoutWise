    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutWise.Domain.Aggregates.ValueObjects;

public abstract record SecuredId<T>
{
    public Guid Value { get; }

    protected SecuredId(Guid value) => Value = value;

    public static T New() => (T)Activator.CreateInstance(typeof(T), Guid.NewGuid())!;

    public override string ToString() => Value.ToString();
}
