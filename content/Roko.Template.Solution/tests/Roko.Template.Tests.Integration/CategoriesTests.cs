namespace Roko.Template.Tests.Integration
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.DependencyInjection;
    using Org.BouncyCastle.Bcpg;
    using Roko.Template.Application.Contracts;
    using Roko.Template.Application.Contracts.Categories;
    using Roko.Template.Domain;
    using Roko.Template.Infrastructure.Db.MyDb.Internal;
    using System.Collections;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Net;
    using System.Reflection;
    using System.Text;

    public class CategoriesTests(ApiWebApplicationFactory factory) : IntegrationTest(factory)
    {
        private const string CategoryApiPath = "api/v1.0/Category";
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async void PostCategory()
        {
            var createCommand = this._fixture.Create<CreateCategoryCommand>();

            var (response, categoryFromResponse) =
                await this.Client
                    .PostAsync(CategoryApiPath, createCommand.AsJsonContent())
                    .WithBody<Category>();

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            categoryFromResponse.Should().BeEquivalentTo(createCommand);
        }

        [Fact]
        public async void PutCategory()
        {
            var category = this._fixture.Create<Category>();
            await DatabaseUtility.SaveToDatabase(category);
            var updateCommand = this._fixture.Create<UpdateCategoryCommand>() with { Id = category.Id };

            var (response, categoryFromResponse) =
                await this.Client
                    .PutAsync(CategoryApiPath, updateCommand.AsJsonContent())
                    .WithBody<Category>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            categoryFromResponse.Should().BeEquivalentTo(updateCommand);
        }

        [Fact]
        public async void PutCategory_WhenNotFound()
        {
            var updateCommand = this._fixture.Create<UpdateCategoryCommand>();

            var response = await
                this.Client
                    .PutAsync(CategoryApiPath, updateCommand.AsJsonContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void GetCategory()
        {
            var someCategories = _fixture.CreateMany<Category>(1).ToArray();
            await DatabaseUtility.SaveToDatabase(someCategories);

            var (response, categoriesFromResponse) =
                await this.Client
                    .GetAsync(CategoryApiPath)
                    .WithBody<IEnumerable<Category>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            categoriesFromResponse.HavingIdsMatchingTo(someCategories).Should().BeEquivalentTo(someCategories);
        }

        [Fact]
        public async void GetCategoryById()
        {
            var category = this._fixture.Create<Category>();
            await DatabaseUtility.SaveToDatabase(category);

            var (response, categoryFromResponse) = await
                this.Client
                    .GetAsync($"{CategoryApiPath}/{category.Id}")
                    .WithBody<Category>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            categoryFromResponse.Should().BeEquivalentTo(category);
        }

        [Fact]
        public async void GetCategoryById_WhenNotFound()
        {
            var id = this._fixture.Create<Guid>();

            var response = await
                this.Client
                    .GetAsync($"{CategoryApiPath}/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}