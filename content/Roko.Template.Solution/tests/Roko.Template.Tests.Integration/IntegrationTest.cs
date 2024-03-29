namespace Roko.Template.Tests.Integration
{
    [Trait("Category", "Integration")]
    public abstract class IntegrationTest(ApiWebApplicationFactory factory) : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory Factory = factory;

        protected DatabaseUtility Database { get; } = new DatabaseUtility(factory);

        protected HttpUtility Http { get; } = new HttpUtility(factory.CreateClient());
    }
}