namespace Roko.Template.Application.Categories
{
    using Roko.Template.Application.Contracts;
    using Roko.Template.Domain;
    using FluentValidation;
    using MediatR;
    using Roko.Template.Application.Contracts.Categories;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            this.RuleFor(r => r.Name).NotEmpty();

            this.RuleFor(r => r.Description).NotEmpty();

            this.RuleFor(r => r.Color).NotEmpty();

            this.RuleFor(r => r.Icon).NotEmpty();

            this.RuleFor(r => r.Amount).GreaterThan(0);
        }
    }

    internal sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = await this.unitOfWork.Categories.GetByIdAsync(request.Id, cancellationToken);

            category.Update(
                request.Name,
                request.Description,
                request.Color,
                request.Icon,
                request.Amount);

            this.unitOfWork.Categories.Update(category);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            return category;
        }
    }
}
