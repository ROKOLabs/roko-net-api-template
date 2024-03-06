namespace Roko.Template.Application.Contracts
{
    namespace Categories
    {
        using Roko.Template.Domain;

        public record CreateCategoryCommand(string Name, string Description, string Color, string Icon, decimal Amount)
            : IRequest<Category>;

        public record GetCategoriesQuery() : IRequest<List<Category>>;

        public record GetCategoryByIdQuery(Guid Id) : IRequest<Category>;

        public record UpdateCategoryCommand(
            Guid Id,
            string Name,
            string Description,
            string Color,
            string Icon,
            decimal Amount) : IRequest<Category>;
    }
}