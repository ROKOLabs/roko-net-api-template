namespace Roko.Template.Tests.Integration
{
    using MediatR;
    using Newtonsoft.Json;
    using Roko.Template.Domain;
    using System.Text;

    public static class IntegrationTestHelpers
    {
        public static IEnumerable<T> HavingIdsMatchingTo<T>(this IEnumerable<T> all, IEnumerable<T> relevant)
            where T: IResource
        {
            var relevantIds = relevant.Select(r => r.Id);
            return all.Where(r => relevantIds.Contains(r.Id));
        }
    }
}