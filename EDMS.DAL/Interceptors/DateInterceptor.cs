using EveryDaily.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace EveryDaily.DAL.Interceptors
{
    public class DateInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var dbContext = eventData.Context;
            if(dbContext == null)
            {
                return base.SavingChanges(eventData, result);
            }

            var entries = dbContext.ChangeTracker.Entries<IAuditable>();

            foreach (var entry in entries)
            {
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                {
                    entry.Property(x=>x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }

                if(entry.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                {
                    entry.Property(x=>x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SavingChanges(eventData, result);
        }
    }
}
