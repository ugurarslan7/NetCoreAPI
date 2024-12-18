using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Intereceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        private static Dictionary<EntityState, Action<DbContext, IBaseAuditEntity>> _behavior = new()
        {
            {EntityState.Added,AddBehavior},
            {EntityState.Added,ModifiedBehavior}
        };

        private static void AddBehavior(DbContext context, IBaseAuditEntity entity)
        {
            entity.Created = DateTime.Now;
            context.Entry(entity).Property(p => p.Updated).IsModified = false;
        }

        private static void ModifiedBehavior(DbContext context, IBaseAuditEntity entity)
        {
            entity.Updated = DateTime.Now;
            context.Entry(entity).Property(p => p.Created).IsModified = false;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            foreach (var item in eventData.Context.ChangeTracker.Entries().ToList())
            {
                if (item.Entity is not IBaseAuditEntity _baseAuditEntity) continue;

                if (item.State is not EntityState.Added or EntityState.Modified) continue;

                _behavior[item.State](eventData.Context, _baseAuditEntity);

            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
