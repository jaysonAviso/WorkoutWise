using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutWise.Domain.Common.Entities
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
