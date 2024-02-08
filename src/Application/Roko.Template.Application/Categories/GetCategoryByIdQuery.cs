namespace Roko.Template.Application.Categories
{
    using Roko.Template.Application.Contracts;
    using Roko.Template.Domain;
    using FluentValidation;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public record GetCategoryByIdQuery(Guid Id) : IRequest<Category>;

    internal sealed class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            // If needed, add validation.
        }
    }

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
