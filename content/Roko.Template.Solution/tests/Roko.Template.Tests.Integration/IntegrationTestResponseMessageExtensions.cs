namespace Roko.Template.Tests.Integration
{
    using Newtonsoft.Json;

    public static class IntegrationTestResponseMessageExtensions
    {
        public static async Task<T> GetFromBody<T>(this HttpResponseMessage responseMessage)
        {
            string body = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(body)!;
        }
    }
}