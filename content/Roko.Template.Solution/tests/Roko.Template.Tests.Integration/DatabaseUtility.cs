namespace Roko.Template.Tests.Integration
{
    using Microsoft.Extensions.DependencyInjection;
    using Roko.Template.Infrastructure.Db.MyDb.Internal;

    public class DatabaseUtility(ApiWebApplicationFactory factory)
    {
        public async Task SaveToDatabase<T>(params T[] items)
            where T:class
        {
            using var scope = factory.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            foreach (var item in items)
            {
                dbContext.Set<T>().Add(item);
            }

            await dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}