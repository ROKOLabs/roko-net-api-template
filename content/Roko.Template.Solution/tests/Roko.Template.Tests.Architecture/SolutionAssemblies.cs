namespace Roko.Template.Tests.Architecture
{
    using FluentAssertions;
    using Roko.Template.Application.Contracts;
    using Roko.Template.Domain.Seedwork;
    using System.Reflection;

    public static class SolutionAssemblies
    {
        public static readonly IEnumerable<Assembly> Application = new[]
        {
            typeof(Roko.Template.Application.Module).Assembly, typeof(IUnitOfWork).Assembly
        };

        public static readonly IEnumerable<Assembly> ApplicationContracts =
            Application.Where(a => a.FullName!.Contains("Contracts"));
        
        public static readonly IEnumerable<Assembly> Blocks = new[]
        {
            typeof(Blocks.Common.Exceptions.ServiceValidationException).Assembly,
            typeof(Blocks.Common.Kernel.SystemClock).Assembly
        };

        public static readonly IEnumerable<Assembly> Domain = new[] { typeof(Enumeration).Assembly };

        public static readonly IEnumerable<Assembly> Infrastructure = new[]
        {
            typeof(Roko.Template.Infrastructure.Db.MyDb.Module).Assembly
        };

        public static readonly IEnumerable<Assembly> Presentation = new[]
        {
            typeof(Roko.Template.Presentation.Api.Module).Assembly
        };

        public static readonly IEnumerable<Assembly> All =
            [..Application, ..Blocks, ..Domain, ..Infrastructure, ..Presentation];

        public static void CanReferenceOnly(this IEnumerable<Assembly> sourceAssemblies, params IEnumerable<Assembly>[] allowedAssemblies)
        {
            var notAllowedAssemblies = All.Except(allowedAssemblies.SelectMany(list => list));

            var pairs =
                from sourceAssembly in sourceAssemblies
                from notAllowedAssembly in notAllowedAssemblies
                select (sourceAssembly, notAllowedAssembly);

            foreach (var pair in pairs)
            {
                pair.sourceAssembly.Should().NotReference(pair.notAllowedAssembly);
            }
        }
    }
}