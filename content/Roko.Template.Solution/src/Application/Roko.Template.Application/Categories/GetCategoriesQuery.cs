namespace Roko.Template.Application.Categories
{
    using Roko.Template.Application.Contracts;
    using Roko.Template.Domain;
    using FluentValidation;
    using MediatR;
    using Roko.Template.Application.Contracts.Categories;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
    {
        public GetCategoriesQueryValidator()
        {
            // If needed, add validation.
        }
    }

    internal sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<Category>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await this.unitOfWork.Categories.GetCategoriesAsync(cancellationToken);
        }
    }
}
