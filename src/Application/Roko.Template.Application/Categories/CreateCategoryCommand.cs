namespace Roko.Template.Application.Categories
{
    using Roko.Template.Application.Contracts;
    using Roko.Template.Domain;
    using FluentValidation;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public record CreateCategoryCommand(string Name, string Description, string Color, string Icon, decimal Amount) : IRequest;

    internal sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            this.RuleFor(r => r.Name).NotEmpty();

            this.RuleFor(r => r.Description).NotEmpty();

            this.RuleFor(r => r.Color).NotEmpty();

            this.RuleFor(r => r.Icon).NotEmpty();

            this.RuleFor(r => r.Amount).GreaterThan(0);
        }
    }

    internal sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = new(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.Color,
                request.Icon,
                request.Amount);

            this.unitOfWork.Categories.Add(category);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
