namespace Roko.Template.Infrastructure.Db.Mssql.Internal
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using System;

    internal sealed class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

            optionsBuilder.UseNpgsql(null);

            var instance = new MyDbContext(optionsBuilder.Options);

            return instance is null ? throw new InvalidOperationException($"Unable to initialize {nameof(MyDbContext)} instance.") : instance;
        }
    }
}
