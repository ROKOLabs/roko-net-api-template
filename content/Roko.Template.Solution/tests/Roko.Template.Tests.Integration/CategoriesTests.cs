namespace Roko.Template.Tests.Integration
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.DependencyInjection;
    using Org.BouncyCastle.Bcpg;
    using Roko.Template.Application.Contracts;
    using Roko.Template.Domain;
    using Roko.Template.Infrastructure.Db.MyDb.Internal;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Net;
    using System.Reflection;
    using System.Text;

    public class CategoriesTests(ApiWebApplicationFactory factory) : IntegrationTest(factory)
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async void GetCategory()
        {
            var someCategories = _fixture.CreateMany<Category>(10).ToArray();
            await DatabaseUtility.SaveToDatabase(someCategories);

            var categoriesMessage = await Client.GetAsync("api/v1.0/Category");
            var categories = await categoriesMessage.GetFromBody<IEnumerable<Category>>();

            categoriesMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            categories.Should().BeEquivalentTo(someCategories);
        }
    }
}