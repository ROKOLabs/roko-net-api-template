namespace Roko.Template.Application.Contracts
{
    using Roko.Template.Domain;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICategoryRepository
    {
        void Add(Category category);

        void Update(Category category);

        void Delete(Category category);

        Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken);
    }
}
