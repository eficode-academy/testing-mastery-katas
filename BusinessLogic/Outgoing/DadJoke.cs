using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Backend.Outgoing
{
    public class DadJoke : Joke
    {

        private static readonly HttpClient client = new HttpClient();

        public DadJoke()
        {
            client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string? Fetch()
        {
            var response = JsonConvert.DeserializeObject<DataType.DadJoke>(client.GetStringAsync("https://icanhazdadjoke.com/").Result);
            return response?.Joke;
        }
    }
}