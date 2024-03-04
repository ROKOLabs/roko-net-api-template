namespace Roko.Template.Infrastructure.Db.MyDb.Internal.Configurations
{
    using Roko.Template.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        private const string TableName = "Category";

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            builder.Property(c => c.Description).IsRequired().HasMaxLength(100);

            builder.Property(c => c.Icon).IsRequired().HasMaxLength(50);

            builder.Property(c => c.Color).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Amount).IsRequired();
        }
    }
}
