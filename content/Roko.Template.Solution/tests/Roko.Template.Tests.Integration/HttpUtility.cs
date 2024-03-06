namespace Roko.Template.Tests.Integration
{
    using MediatR;
    using Newtonsoft.Json;
    using System.Text;

    public class HttpUtility(HttpClient client)
    {
        public async Task<ResponseWithBody<TBody>> PostAsync<TBody>(string url, IRequest<TBody> request)
        {
            return await WithBody<TBody>(
                await client.PostAsync(url, JsonContent(request)));
        }
        
        public async Task<ResponseWithBody<TBody>> PutAsync<TBody>(string url, IRequest<TBody> request)
        {
            return await WithBody<TBody>(
                await client.PutAsync(url, JsonContent(request)));
        }

        public async Task<ResponseWithBody<TBody>> GetAsync<TBody>(string url)
        {
            return await WithBody<TBody>(
                await client.GetAsync(url));
        }

        private static StringContent JsonContent(object body)
        {
            return new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json");
        }

        private static async Task<ResponseWithBody<T>> WithBody<T>(HttpResponseMessage responseMessage)
        {
            var bodyString = await responseMessage.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<T>(bodyString)!;

            return new ResponseWithBody<T>(
                responseMessage,
                body);
        }

        public record ResponseWithBody<T>(HttpResponseMessage Message, T Body)
        {
            public void Deconstruct(out HttpResponseMessage message, out T body)
            {
                message = this.Message;
                body = this.Body;
            }
        }
    }
}