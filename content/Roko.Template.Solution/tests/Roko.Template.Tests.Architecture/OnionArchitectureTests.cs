namespace Roko.Template.Tests.Architecture
{
    using static SolutionAssemblies;

    [Trait("Category", "Architecture")]
    public class OnionArchitectureTests
    {
        [Fact]
        public void Domain_References()
        {
            Blocks.CanReferenceOnly(Blocks);
            Domain.CanReferenceOnly(Blocks, Domain);
            Application.CanReferenceOnly(Blocks, Domain, Application);
            Infrastructure.CanReferenceOnly(Blocks, Domain, ApplicationContracts);
            Presentation.CanReferenceOnly(Blocks, Domain, ApplicationContracts);
        }
    }
}