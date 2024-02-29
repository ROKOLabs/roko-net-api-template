namespace Roko.Template.Infrastructure.Db.MyDb.Internal
{
    using Roko.Template.Application.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext context;

        public UnitOfWork(
            MyDbContext context,
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
