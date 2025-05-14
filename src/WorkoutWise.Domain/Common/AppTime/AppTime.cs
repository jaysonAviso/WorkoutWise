using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutWise.Domain.Common.AppTime
{
    public static class AppTime
    {
        public static DateTime UtcNow => DateTime.UtcNow;
        public static readonly DateTime DefaultDate = new(1900,1,1);
        public static readonly DateOnly DafaultDateOnly = new(1900, 1, 1);
    }
}
