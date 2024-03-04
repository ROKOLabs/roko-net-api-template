namespace Roko.Template.Infrastructure.Db.MyDb.Internal
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using System;

    internal sealed class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

#if (MyDb)
            optionsBuilder.UseNpgsql(null);
#elif (Postgres)
            optionsBuilder.UseNpgsql(null);
#elif (MsSql)
            optionsBuilder.UseSqlServer(null);
#else
    #error Database not supported, define project constant or template parameter with the right value
#endif

            var instance = new MyDbContext(optionsBuilder.Options);

            return instance is null ? throw new InvalidOperationException($"Unable to initialize {nameof(MyDbContext)} instance.") : instance;
        }
    }
}
