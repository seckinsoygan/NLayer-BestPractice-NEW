using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Repositories.Interceptors;

public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> StateActions = new()
    {
        { EntityState.Added, AddBehavior },
        { EntityState.Modified, UpdateBehavior }
    };

    private static void AddBehavior(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.Created = DateTime.UtcNow;
        context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
    }

    private static void UpdateBehavior(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.Updated = DateTime.UtcNow;
        context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context!);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context!);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void ApplyAudit(DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries()
                     .Where(e => e.Entity is IAuditEntity
                                 && (e.State == EntityState.Added || e.State == EntityState.Modified)))
        {
            var auditEntity = (IAuditEntity)entry.Entity;

            // Sözlükten güvenli çağrı
            if (StateActions.TryGetValue(entry.State, out var action))
                action(context, auditEntity);
        }
    }
}
