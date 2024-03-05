namespace Roko.Template.Tests.Integration
{
    [Trait("Category", "Integration")]
    public abstract class IntegrationTest(ApiWebApplicationFactory factory) : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory Factory = factory;

        protected HttpClient Client { get; } = factory.CreateClient();

        protected DatabaseUtility DatabaseUtility { get; } = new DatabaseUtility(factory);
    }
}