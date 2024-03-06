namespace Roko.Template.Tests.Integration
{
    using Roko.Template.Domain;

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