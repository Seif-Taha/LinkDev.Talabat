using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructure.Persistence.Interceptors
{
    public class BaseAuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly ILoggedInUserService _loggedInUserService;


        public BaseAuditableEntityInterceptor(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
        }


        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }


        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {

            UpdateEntities(eventData.Context);

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }


        private void UpdateEntities(DbContext? dbContext)
        {

            if (dbContext is null) return;

            var utcNow = DateTime.UtcNow;

            foreach (var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>())
            {

                if (entry is { State: EntityState.Added or EntityState.Modified })
                {

                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedBy = "";
                        entry.Entity.CreatedOn = utcNow;
                    }

                    entry.Entity.LastModifiedBy = "";
                    entry.Entity.LastModifiedOn = utcNow;
                }


            }


        }


    }
}
