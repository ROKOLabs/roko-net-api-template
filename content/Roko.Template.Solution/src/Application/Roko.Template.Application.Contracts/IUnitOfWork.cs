namespace Roko.Template.Application.Contracts
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
