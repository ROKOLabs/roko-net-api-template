namespace Roko.Template.Tests.Integration
{
    using Newtonsoft.Json;
    using Roko.Template.Domain;
    using System.Text;

    public static class IntegrationTestHelpers
    {
        public static StringContent AsJsonContent(this object body)
        {
            return new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json");
        }

        public static async Task<(HttpResponseMessage, T)> WithBody<T>(this Task<HttpResponseMessage> responseMessage)
        {
            var response = await responseMessage;
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<T>(body)!;

            return (response, jsonBody);
        }

        public static IEnumerable<T> HavingIdsMatchingTo<T>(this IEnumerable<T> all, IEnumerable<T> relevant)
            where T: IResource
        {
            var relevantIds = relevant.Select(r => r.Id);
            return all.Where(r => relevantIds.Contains(r.Id));
        }
    }
}