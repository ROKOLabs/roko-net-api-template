namespace Roko.Template.Application.Categories
{
    using Roko.Template.Application.Contracts;
    using Roko.Template.Domain;
    using FluentValidation;
    using MediatR;
    using Roko.Template.Application.Contracts.Categories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>;

    internal sealed class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.unitOfWork.Categories.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
