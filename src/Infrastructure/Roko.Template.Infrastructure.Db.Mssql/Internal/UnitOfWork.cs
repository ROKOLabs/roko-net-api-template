namespace Roko.Template.Infrastructure.Db.Mssql.Internal
{
    using Roko.Template.Application.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly MssqlDbContext context;

        public UnitOfWork(
            MssqlDbContext context,
            ICategoryRepository categoryRepository)
        {
            this.context = context;
            this.Categories = categoryRepository;
        }

        public ICategoryRepository Categories { get; }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
