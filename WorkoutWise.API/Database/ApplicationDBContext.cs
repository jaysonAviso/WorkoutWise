﻿using Microsoft.EntityFrameworkCore;

namespace WorkoutWise.Database
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
    }
}
